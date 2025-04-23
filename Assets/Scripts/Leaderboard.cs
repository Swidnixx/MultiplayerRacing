using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct Car
{
    public string name;
    public int position;

    public Car(string name, int position)
    {
        this.name = name;
        this.position = position;
    }
}

public class Leaderboard
{
    static Dictionary<int, Car> board = new Dictionary<int, Car>();

    public static void Reset()
    {
        board.Clear();
    }

    public static void RegisterCar(int number, string name)
    {
        board.Add(number, new Car(name, 0));
    }
    public static void DropPlayer(int number)
    {
        board.Remove(number);
    }

    public static void SetPosition(int rego, int lap, int checkPoint)
    {
        int position = lap * 1000 + checkPoint;
        board[rego] = new Car(board[rego].name, position);
    }

    public static List<string> GetPlaces()
    {
        List<string> places = new List<string>();
        foreach (var pos in board.OrderByDescending(key => key.Value.position))
        {
            places.Add(pos.Value.name);
        }
        return places;
    }
}
