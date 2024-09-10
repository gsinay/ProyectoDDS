using Fire_Emblem_View;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.Effects;


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
                [new InitiatingCombatCondition()],
                [new StatChangeEffect("Atk", 6, _view, applyToBoth: true)]
            ),
            "will to win" => new Skill(
                "Will to Win",
                "Si el HP de la unidad está al 50% o menos al inicio del combate, otorga Atk+8 durante el combate.",
                [new LowHpCondition(0.5)],
                [new StatChangeEffect("Atk", 8, _view)]
            ),
            "single-minded" => new Skill(
                "Single-Minded",
                "En un combate contra un rival que también es el oponente más reciente de la unidad, otorga Atk+8 durante el combate.",
                [new SameOpponentCondition()],
                [new StatChangeEffect("Atk", 8, _view)]
            ),
            "ignis" => new Skill(
                "Ignis",
                "Otorga Atk+50% al primer ataque de la unidad.",
                [new FirstAttackCondition()],
                [new FirstAttackBoostEffect(50)]
            ),
            "perceptive" => new Skill(
                "Perceptive",
                "Si la unidad inicia el combate, otorga Spd+12 a la unidad durante el combate, y por cada cuatro puntos de Spd (sin contar bonus o penalty), la unidad gana Spd+1 adicional.",
                [new InitiatingCombatCondition()],
                [
                    new StatChangeEffect("Spd", 12, _view),
                    new ScalingStatBoostEffect("Spd", 4)
                ]
            ),
            "tome precision" => new Skill(
                "Tome Precision",
                "Otorga Atk/Spd+6 al usar magia.",
                [new UsingSpecificWeaponCondition("Magic")],
                [
                    new StatChangeEffect("Atk", 6, _view),
                    new StatChangeEffect("Spd", 6, _view)
                ]
            ),
            "attack +6" => new Skill(
                "Attack +6",
                "Otorga Atk+6",
                [],
                [new StatChangeEffect("Atk", 6, _view)]
            ),
            "speed +5" => new Skill(
                "Speed +5",
                "Otorga Spd+5",
                [],
                [new StatChangeEffect("Spd", 5, _view)]
            ),
            "defense +5" => new Skill(
                "Defense +5",
                "Otorga Def+5",
                [],
                [new StatChangeEffect("Def", 5, _view)]
            ),
            "wrath" => new Skill(
                "Wrath",
                "Al inicio del combate, por cada punto de HP que la unidad ha perdido, otorga Atk/Spd+1" +
                "durante el combate. (Max +30)",
                [new AlwaysTrueCondition()],
                [new WrathEffect()]
            ),
            "resolve" => new Skill(
                "Resolve",
                "Si el HP de la unidad es <= a al 75 % o menos al inicio del combate, otorga Def/Res+7",
                [new LowHpCondition(0.75)],
                [
                    new StatChangeEffect("Def", 7, _view),
                    new StatChangeEffect("Res", 7, _view)
                ]),
            "resistance +5" => new Skill(
                "Resistance +5",
                "Otorga Res+5",
                [],
                [new StatChangeEffect("Res", 5, _view)]
            ),
            "atk/def +5" => new Skill(
                "Atk/Def +5", 
                "Otorga Atk+5 y Def+5",
                [],
                [
                    new StatChangeEffect("Atk", 5, _view),
                    new StatChangeEffect("Def", 5, _view)
                ]),
            "atk/res +5" => new Skill(
                "Atk/Res +5", 
                "Otorga Atk+5 y Res+5",
                [],
                [
                    new StatChangeEffect("Atk", 5, _view),
                    new StatChangeEffect("Res", 5, _view)
                ]),
            "spd/res +5" => new Skill(
                "Spd/Res +5", 
                "Otorga Spd+5 y Res+5",
                [],
                [
                    new StatChangeEffect("Spd", 5, _view),
                    new StatChangeEffect("Res", 5, _view)
                ]),
            "deadly blade" => new Skill(
                "Deadly blade",
                "Si la unidad inicia el combate con una espada, otorga Atk/Spd+8 durante el combate",
                [
                    new InitiatingCombatCondition(),
                    new UsingSpecificWeaponCondition("Sword")
                ],
                [
                    new StatChangeEffect("Atk", 8, _view),
                    new StatChangeEffect("Spd", 8, _view)
                ]), 
            "death blow" => new Skill(
                "Death blow",
                "Si la unidad inicia el combate, otorga Atk+8 durante el combate.",
                [new InitiatingCombatCondition()],
                [new StatChangeEffect("Atk", 8, _view)]
                ),
            "armored blow" => new Skill(
                "Armored blow",
                "Si la unidad inicia el combate, otorga Def+8 durante el combate.",
                [new InitiatingCombatCondition()],
                [new StatChangeEffect("Def", 8, _view)]
            ),
            "darting blow" => new Skill(
                "Darting blow",
                "Si la unidad inicia el combate, otorga Spd+8 durante el combate.",
                [new InitiatingCombatCondition()],
                [new StatChangeEffect("Spd", 8, _view)]
            ),
            "warding blow" => new Skill(
                "Warding blow",
                "Si la unidad inicia el combate, otorga Res+8 durante el combate.",
                [new InitiatingCombatCondition()],
                [new StatChangeEffect("Res", 8, _view)]
            ),
            "swift sparrow" => new Skill(
                "Swift Sparrow",
                " Si la unidad inicia el combate, otorga Atk/Spd+6 durante el combate.",
                [new InitiatingCombatCondition()],
                [
                    new StatChangeEffect("Atk", 6, _view),
                    new StatChangeEffect("Spd", 6, _view)
                ]),
            "sturdy blow" => new Skill(
                "Sturdy Blow",
                "Si la unidad inicia el combate, otorga Atk/Def+6 durante el combate",
                [new InitiatingCombatCondition()],
                [
                new StatChangeEffect("Atk", 6, _view),
                new StatChangeEffect("Def", 6, _view)
                ]),
            "mirror strike" => new Skill(
                "Mirror Strike",
                "Si la unidad inicia el combate, otorga Atk/Res+6 durante el combate",
                [new InitiatingCombatCondition()],
                [
                new StatChangeEffect("Atk", 6, _view),
                new StatChangeEffect("Res", 6, _view)
                ]),
            "steady blow" => new Skill(
                "Steady Blow",
                "Si la unidad inicia el combate, otorga Spd/Def+6 durante el combate.",
                [new InitiatingCombatCondition()],
                [
                new StatChangeEffect("Spd", 6, _view),
                new StatChangeEffect("Def", 6, _view)
                ]),
            "swift strike" => new Skill(
                "Swift Strike",
                "Si la unidad inicia el combate, otorga Spd/Res+6 durante el combate.",
                [new InitiatingCombatCondition()],
                [
                new StatChangeEffect("Spd", 6, _view),
                new StatChangeEffect("Res", 6, _view)
                ]),
            "bracing blow" => new Skill(
                "Bracing Blow",
                "Si la unidad inicia el combate, otorga Def/Res+6 durante el combate.",
                [new InitiatingCombatCondition()],
                [
                new StatChangeEffect("Def", 6, _view),
                new StatChangeEffect("Res", 6, _view)
                ]),
            "brazen atk/spd" => new Skill(
                "Brazen Atk/Spd",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Spd+10 durante el combate.",
                [new LowHpCondition(0.8)],
                [
                    new StatChangeEffect("Atk", 10, _view),
                    new StatChangeEffect("Spd", 10, _view)
                ]),
            "brazen atk/def" => new Skill(
                "Brazen Atk/Def",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Def+10 durante el combate.",
                [new LowHpCondition(0.8)],
                [
                    new StatChangeEffect("Atk", 10, _view),
                    new StatChangeEffect("Def", 10, _view)
                ]),
            "brazen atk/res" => new Skill(
                "Brazen Atk/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Res+10 durante el combate.",
                [new LowHpCondition(0.8)],
                [
                    new StatChangeEffect("Atk", 10, _view),
                    new StatChangeEffect("Res", 10, _view)
                ]),
            "brazen spd/def" => new Skill(
                "Brazen Spd/Def",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Spd/Def+10 durante el combate.",
                [new LowHpCondition(0.8)],
                [
                    new StatChangeEffect("Spd", 10, _view),
                    new StatChangeEffect("Def", 10, _view)
                ]),
            "brazen spd/res" => new Skill(
                "Brazen Spd/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Spd/Res+10 durante el combate.",
                [new LowHpCondition(0.8)],
                [
                    new StatChangeEffect("Spd", 10, _view),
                    new StatChangeEffect("Res", 10, _view)
                ]),
            "brazen def/res" => new Skill(
                "Brazen Def/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Def/Res+10 durante el combate.",
                [new LowHpCondition(0.8)],
                [
                    new StatChangeEffect("Def", 10, _view),
                    new StatChangeEffect("Res", 10, _view)
                ]),
            "fire boost" => new Skill(
                "Fire Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Atk+6 durante el combate",
                [new GreaterHpCondition(3)],
                [new StatChangeEffect("Atk", 6, _view)]
                ),
            "wind boost" => new Skill(
                "Wind Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Spd+6 durante el combate",
                [new GreaterHpCondition(3)],
                [new StatChangeEffect("Spd", 6, _view)]
            ),
            "earth boost" => new Skill(
                "Eire Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Def+6 durante el combate",
                [new GreaterHpCondition(3)],
                [new StatChangeEffect("Def", 6, _view)]
            ),
            "water boost" => new Skill(
                "Water Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Res+6 durante el combate",
                [new GreaterHpCondition(3)],
                [new StatChangeEffect("Res", 6, _view)]
            ),
            "chaos style" => new Skill(
                "Chaos Style",
                "Si la unidad inicia el combate con un ataque fisico contra un rival armado con magia," +
                " o viceversa, otorga Spd+3 durante el combate.",
                [
                    new OpposingWeaponTypeCondition(),
                    new InitiatingCombatCondition()
                ],
                [new StatChangeEffect("Spd", 3, _view)]
                ),
            "blinding flash" => new Skill(
                "Blinding Flash", 
                "Si la unidad inicia el combate, inflige Spd-4 en el rival durante el combate",
                [new InitiatingCombatCondition()],
                [new StatChangeEffect("Spd", 4, _view, applyToOpponent: true)]
                ),
            "not *quite*" => new Skill(
                "Not *Quite*",
                "Si el rival inicia el combate, inflige Atk-4 en ese rival durante el combate",
                [new InitiatingCombatCondition(rivalStarting: true)],
                [new StatChangeEffect("Atk", 4, _view, applyToOpponent: true )]
                ),
            "stunning smile" => new Skill(
                "Stunning Smile", 
                "Si el rival es hombre, inflige Spd-8 en ese rival durante el combate.",
                [new IsMaleCondition(rivalGender: true)],
                [new StatChangeEffect("Spd", 8, _view, applyToOpponent: true)]
                ),
            "disarming sigh" => new Skill(
                "Disarming Sigh", 
                "Si el rival es hombre, inflige Atk-8 en ese rival durante el combate.",
                [new IsMaleCondition(rivalGender: true)],
                [new StatChangeEffect("Atk", 8, _view, applyToOpponent: true)]
            ),
            "charmer" => new Skill(
                "Charming",
                "En un combate contra un rival que tambi \u0301en es el oponente mas " +
                "reciente de la unidad, inflige Atk/Spd-3 en ese rival durante el combate.",
                [new SameOpponentCondition()],
                [
                    new StatChangeEffect("Atk", 3, _view, applyToOpponent:true),
                    new StatChangeEffect("Spd", 3, _view, applyToOpponent:true)
                ]
                ),
            "luna" => new Skill(
                "Luna",
                "Durante el primer ataque de la unidad, ignora la mitad de Def y Res base del rival. " +
                "(Considere esta reduccion como un Penalty",
                [new AlwaysTrueCondition()],
                [new LunaPenaltyEffect()]
                ),
            "belief in love" => new Skill(
                "Belief in love",
                "Si el rival inicia el combate o tiene HP=100 % al inicio del combate, " +
                "inflige Atk/Def-5 en el rival durante el combate.",
                [new CompositeCondition([new InitiatingCombatCondition(rivalStarting: true), new HighHpCondition(1)], useOr: true)],
                [
                    new StatChangeEffect("Atk", 5, _view, applyToOpponent:true),
                    new StatChangeEffect("Def", 5, _view, applyToOpponent:true),
                ]),
            _ => new UnimplementedSkill(skillName)
        };
        
        
    }
}