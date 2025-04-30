using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] placeablePrefabs;
    private int currentIndex = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            // no addition can be made outside grass area
            if (Input.mousePosition.x < 150f)
                return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos2D = new Vector2(mousePos.x, mousePos.y);

            // ALLOW ONLY IF click hits "Grass"
            RaycastHit2D hit = Physics2D.Raycast(clickPos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Grass"))
            {
                Vector3 spawnPos = new Vector3(mousePos.x, mousePos.y, 0f);
                GameObject newPlant = Instantiate(placeablePrefabs[currentIndex], spawnPos, Quaternion.identity);
                GardenManager.Instance.RegisterPlant(newPlant);

            }
        }
    }

    public void SelectObject(int index)
    {
        currentIndex = index;
    }
}
