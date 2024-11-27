
using Fire_Emblem_GUI;

namespace Fire_Emblem.Models.Player;

public class MyUnit(string name, string weapon, int hp, int atk, int spd, int def, int res) : IUnit
{
    public string Name { get; } = name;
    public string Weapon { get; } = weapon; public int Hp { get; set; } = hp;
    public int Atk { get; } = atk; public int Spd { get; } = spd;
    public int Def { get; } = def; public int Res { get; } = res;
    public string[] Skills { get; } = []; }