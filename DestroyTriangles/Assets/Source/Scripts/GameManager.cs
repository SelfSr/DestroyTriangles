using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // библиотека для перехода между сценами

public class GameManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D BirdRigid; // Rigidbody для птицы
    [SerializeField] public Rigidbody2D ShootRigid; // Rigidbody для рогатки

    [SerializeField] public GameObject BirdPrefab; // префаб для птицы
    [SerializeField] public Transform BirdSpawnerPos; // позиция птицы

    [SerializeField] private float maxDistance = 3f; // максимальный радиус окружности, куда можно увести снаряд

    [SerializeField] private bool isPressed = false; // нажатие кнопки (изначально не нажата)

    private void Start()
    {
        BirdRigid = GetComponent<Rigidbody2D>(); // Птица получает компонент Rigidbody2D
    }

    private void Update()
    {
        if (isPressed == true) // если кнопка нажата
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // новый вектор, который равен курсору мыши

            if (Vector2.Distance(mousePos, ShootRigid.position) > maxDistance) // если дистанция между курсором и рогаткой больше, чем максимальная дистанция
            {
                BirdRigid.position = ShootRigid.position + (mousePos - ShootRigid.position).normalized * maxDistance; // позиция птички будет на границе окружности (чекай ролик, если не понимаешь)
            }

            else
            {
                BirdRigid.position = mousePos; // если превышения нет, то птичка свободно перемещается в воорброжаемой окружности
            }
        }
    }

    private void OnMouseDown() // если кнопка мыши нажата (также работает на смартфоне)
    {
        isPressed = true; // кнопка нажата (проверка)
        BirdRigid.isKinematic = true; // птичка становится кинематической (не болтается)
    }

    private void OnMouseUp() // если кнопка мыши отжата/отпущена (также работает на смартфоне)
    {
        isPressed = false; // кнопка не нажата (проверка)
        BirdRigid.isKinematic = false; // птичка болтается на веревке

        StartCoroutine(LetGo()); // запуск корутины
    }

    IEnumerator LetGo()
    {
        yield return new WaitForSeconds(0.1f); // ждем очень мало времени

        gameObject.GetComponent<SpringJoint2D>().enabled = false; // выключается компонент "Веревка" (ну по-русски если), и птица улетает
        this.enabled = false; // сам скрипт тоже отключается, чтобы мы не могли трогать птицу, когда она уже улетела
        Destroy(gameObject, 5); // объект удаляется через пять секунд после того, как был отпущен

        yield return new WaitForSeconds(2); // ждем две секунды

        if (BirdPrefab != null) // если птицы еще есть (или грубо говоря, если количество префаборв не равно 0)
        {
            BirdPrefab.transform.position = BirdSpawnerPos.position; // то птички (которые находятся на земле) перемещаются на рогатку
        }

        else
        {
            SceneManager.LoadScene(0); // если птички кончились, то сцена перезапускается
        }
    }
}
