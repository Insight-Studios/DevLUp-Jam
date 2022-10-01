using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Doors : MonoBehaviour
{

    [SerializeField] Door[] doors;
    [SerializeField] float doorDelay = 0.5f;
    [SerializeField] float winChance = 0.2f;

    GameObject[] numbers;
    IEnumerator revealRoutine = null;

    // Start is called before the first frame update
    void Start()
    {
        numbers = new GameObject[doors.Length];

        numbers[0] = Instantiate(GameManager.NumberOnePrefab.gameObject);
        numbers[0].SetActive(false);

        numbers[0].GetComponent<Collider2D>().enabled = false;

        for (int i = 1; i < numbers.Length; i++)
        {
            numbers[i] = Instantiate(GameManager.NumberOnePrefab.gameObject);
            numbers[i].GetComponent<TextMesh>().text = (i + 1).ToString();
            Destroy(numbers[i].GetComponent<NumberOne>());
            numbers[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Reveal()
    {
        foreach (var number in numbers)
        {
            number.SetActive(true);
        }

        yield return new WaitForSeconds(doorDelay);

        foreach (var door in doors)
        {
            door.Open();
        }

        yield return new WaitForSeconds(doorDelay);

        foreach (var door in doors)
        {
            door.Close();
        }

        foreach (var number in numbers)
        {
            number.SetActive(false);
        }

        numbers[0].GetComponent<Collider2D>().enabled = false;
        revealRoutine = null;
    }

    public void DoorClicked(Door door)
    {
        if (revealRoutine == null)
        {
            var shuffledNumbers = numbers.ToList().OrderBy(number => Random.value).ToArray();
            for (int i = 0; i < doors.Length; i++)
            {
                shuffledNumbers.ElementAt(i).transform.position = doors[i].transform.position;
            }

            int doorIndex = System.Array.IndexOf(doors, door);
            if (Random.value < winChance)
            {
                shuffledNumbers.ElementAt(doorIndex).transform.position = numbers[0].transform.position;
                numbers[0].transform.position = door.transform.position;
                numbers[0].GetComponent<Collider2D>().enabled = true;
            } 
            else if (shuffledNumbers.ElementAt(doorIndex) == numbers[0])
            {
                numbers[0].transform.position = shuffledNumbers.ElementAt((doorIndex + 1) % doors.Length).transform.position;
                shuffledNumbers.ElementAt((doorIndex + 1) % doors.Length).transform.position = door.transform.position;
            }

            door.Open();
            revealRoutine = Reveal();
            StartCoroutine(revealRoutine);
        }
    }
}
