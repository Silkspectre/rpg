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
    public Texture Image;
    public Gender Gender;
}