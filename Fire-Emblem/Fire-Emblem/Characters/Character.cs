using System;
namespace Fire_Emblem;
public class Character
{
    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    
    private int _HP;
    public int Atk { get; set; }
    public int Spd { get; set; }
    public int Def { get; set; }
    public int Res { get; set; }

    private SkillList _Skills;
    
    public Character()
    {
        _Skills = new SkillList();
    }
    public Character(string name, string weapon, string gender,
        string deathQuote, int hp, int atk, int spd, int def, int res)
    {
        Name = name;
        Weapon = weapon;
        Gender = gender;
        DeathQuote = deathQuote;
        _HP = hp;
        Atk = atk;
        Spd = spd;
        Def = def;
        Res = res;
        _Skills = new SkillList();
    }
    
    public void SetHP(int hp)
    {
        _HP = Math.Max(0, hp);
    }
    public void AddSkill(Skill skill)
    {
        _Skills.AddSkill(skill);
    }

    public int SkillCount() => _Skills.Count();

    public List<Skill> GetSkills() => _Skills.GetSkills();

    public bool IsAlive() => _HP > 0;

    public int GetHP => _HP;

    public void TakeDamage(int damage)
    {
         _HP = Math.Max(0, _HP - damage);
    }
}
