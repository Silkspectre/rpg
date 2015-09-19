using UnityEditor;
using UnityEngine;
using System;

public enum Gender
{
    Object,
    Male,
    Female
};

[System.Serializable]
public class Character
{
    public string Name;
    public Sprite Image;
    public Gender Gender;
}