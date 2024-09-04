using Fire_Emblem_View;
using Fire_Emblem.Skills;


namespace Fire_Emblem;

public class SkillFactory
{
    private View _view;
    public SkillFactory(View view)
    {
        _view = view;
    }
    public ISkill GetSkill(string skillName)
    {
        return skillName.ToLower() switch
        {
            "fair fight" => new Skill(
                "Fair Fight",
                "Si la unidad inicia el combate, otorga Atk+6 a la unidad y al rival durante el combate",
                new List<ICondition> { new InitiatingCombatCondition() },
                new List<IEffect> { new StatBoostEffect("Atk", 6, _view, applyToBoth: true) }
            ),
            "will to win" => new Skill(
                "Will to Win",
                "Si el HP de la unidad está al 50% o menos al inicio del combate, otorga Atk+8 durante el combate.",
                new List<ICondition> { new LowHpCondition(0.5) },
                new List<IEffect> { new StatBoostEffect("Atk", 8, _view) }
            ),
            "single-minded" => new Skill(
                "Single-Minded",
                "En un combate contra un rival que también es el oponente más reciente de la unidad, otorga Atk+8 durante el combate.",
                new List<ICondition> { new SameOpponentCondition() },
                new List<IEffect> { new StatBoostEffect("Atk", 8, _view) }
            ),
            "ignis" => new Skill(
                "Ignis",
                "Otorga Atk+50% al primer ataque de la unidad.",
                new List<ICondition> { new FirstAttackCondition() },
                new List<IEffect> { new FirstAttackBoostEffect(50) }
            ),
            "perceptive" => new Skill(
                "Perceptive",
                "Si la unidad inicia el combate, otorga Spd+12 a la unidad durante el combate, y por cada cuatro puntos de Spd (sin contar bonus o penalty), la unidad gana Spd+1 adicional.",
                new List<ICondition> { new InitiatingCombatCondition() },
                new List<IEffect>
                {
                    new StatBoostEffect("Spd", 12, _view), 
                    new ScalingStatBoostEffect("Spd", 4)   
                }
            ),
            "tome precision" => new Skill(
                "Tome Precision",
                "Otorga Atk/Spd+6 al usar magia.",
                new List<ICondition> { new UsingSpecificWeaponCondition("Magic") },
                new List<IEffect>
                {
                    new StatBoostEffect("Atk", 6, _view),
                    new StatBoostEffect("Spd", 6, _view)
                }
            ),
            "attack +6" => new Skill(
                "Attack +6",
                "Otorga Atk+6",
                new List<ICondition>{},
                new List<IEffect> { new StatBoostEffect("Atk", 6, _view)}
                ),
            "speed +5" => new Skill(
                "Speed +5",
                "Otorga Spd+5",
                new List<ICondition>{},
                new List<IEffect> { new StatBoostEffect("Spd", 5, _view)}
            ),
            "defense +5" => new Skill(
                "Defense +5",
                "Otorga Def+5",
                new List<ICondition>{},
                new List<IEffect> { new StatBoostEffect("Def", 5, _view)}
            ),
            "wrath" => new Skill(
                "Wrath",
                "Al inicio del combate, por cada punto de HP que la unidad ha perdido, otorga Atk/Spd+1" +
                "durante el combate. (Max +30)",
                new List<ICondition>{},
                new List<IEffect>{new WrathEffect()}
                ),
            "resolve" => new Skill(
                "Resolve",
                "Si el HP de la unidad es <= a al 75 % o menos al inicio del combate, otorga Def/Res+7",
                new List<ICondition>{new LowHpCondition(0.75)},
                new List<IEffect>
                {
                    new StatBoostEffect("Def", 7, _view),
                    new StatBoostEffect("Res", 7, _view)
                }),
            "resistance +5" => new Skill(
                "Resistance +5",
                "Otorga Res+5",
                new List<ICondition>{},
                new List<IEffect>{new StatBoostEffect("Res", 5, _view)}
                ),
            "atk/def +5" => new Skill(
                "Atk/Def +5", 
                "Otorga Atk+5 y Def+5",
                new List<ICondition>(),
                new List<IEffect>
                {
                    new StatBoostEffect("Atk", 5, _view),
                    new StatBoostEffect("Def", 5, _view)
                }),
            "atk/res +5" => new Skill(
                "Atk/Res +5", 
                "Otorga Atk+5 y Res+5",
                new List<ICondition>(),
                new List<IEffect>
                {
                    new StatBoostEffect("Atk", 5, _view),
                    new StatBoostEffect("Res", 5, _view)
                }),
            "spd/res +5" => new Skill(
                "Spd/Res +5", 
                "Otorga Spd+5 y Res+5",
                new List<ICondition>(),
                new List<IEffect>
                {
                    new StatBoostEffect("Spd", 5, _view),
                    new StatBoostEffect("Res", 5, _view)
                }),
            "deadly blade" => new Skill(
                "Deadly blade",
                "Si la unidad inicia el combate con una espada, otorga Atk/Spd+8 durante el combate",
                new List<ICondition>
                {
                    new InitiatingCombatCondition(), 
                    new UsingSpecificWeaponCondition("Sword")
                },
                new List<IEffect>
                {
                    new StatBoostEffect("Atk", 8, _view),
                    new StatBoostEffect("Spd", 8, _view)
                }), 
            
            _ => new UnimplementedSkill(skillName)
        };
    }
}