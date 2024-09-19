using Fire_Emblem.Characters;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.Conditions;
using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.SkillsManager;

public class SkillFactory
{
    public ISkill GetSkill(string skillName)
    {
        return skillName.ToLower() switch
        {
            "hp +15" => new OneTimeSkill(
                "HP +15", 
                "Otorga max HP+15 (esta habilidad no require ser anunciada).",
                new OneTimeCondition(),
                [new MaxHpBoostEffect(15)]
                ),
            "fair fight" => new Skill(
                "Fair Fight",
                "Si la unidad inicia el combate, otorga Atk+6 a la unidad y al rival durante el combate",
                new InitiatingCombatCondition(),
                [new StatChangeEffect(StatName.Atk, 6, applyToBoth: true)]
            ),
            "will to win" => new Skill(
                "Will to Win",
                "Si el HP de la unidad está al 50% o menos al inicio del combate, otorga Atk+8 durante el combate.",
                new LowHpCondition(0.5),
                [new StatChangeEffect(StatName.Atk, 8)]
            ),
            "single-minded" => new Skill(
                "Single-Minded",
                "En un combate contra un rival que también es el oponente más reciente de la unidad, otorga Atk+8 durante el combate.",
                new SameOpponentCondition(),
                [new StatChangeEffect(StatName.Atk, 8)]
            ),
            "ignis" => new Skill(
                "Ignis",
                "Otorga Atk+50% al primer ataque de la unidad.",
                new FirstAttackCondition(),
                [new FirstAttackBoostEffect(50)]
            ),
            "perceptive" => new Skill(
                "Perceptive",
                "Si la unidad inicia el combate, otorga Spd+12 a la unidad durante el combate, y por cada cuatro puntos de Spd (sin contar bonus o penalty), la unidad gana Spd+1 adicional.",
                new InitiatingCombatCondition(),
                [
                    new StatChangeEffect(StatName.Spd, 12),
                    new ScalingStatBoostEffect(StatName.Spd, 4)
                ]
            ),
            "tome precision" => new Skill(
                "Tome Precision",
                "Otorga Atk/Spd+6 al usar magia.",
                new UsingSpecificWeaponCondition(WeaponName.Magic),
                [
                    new StatChangeEffect(StatName.Atk, 6),
                    new StatChangeEffect(StatName.Spd, 6)
                ]
            ),
            "attack +6" => new Skill(
                "Attack +6",
                "Otorga Atk+6",
                new AlwaysTrueCondition(),
                [new StatChangeEffect(StatName.Atk, 6)]
            ),
            "speed +5" => new Skill(
                "Speed +5",
                "Otorga Spd+5",
                new AlwaysTrueCondition(),
                [new StatChangeEffect(StatName.Spd, 5)]
            ),
            "defense +5" => new Skill(
                "Defense +5",
                "Otorga Def+5",
                new AlwaysTrueCondition(),
                [new StatChangeEffect(StatName.Def, 5)]
            ),
            "wrath" => new Skill(
                "Wrath",
                "Al inicio del combate, por cada punto de HP que la unidad ha perdido, otorga Atk/Spd+1" +
                "durante el combate. (Max +30)",
                new AlwaysTrueCondition(),
                [new WrathEffect()]
            ),
            "resolve" => new Skill(
                "Resolve",
                "Si el HP de la unidad es <= a al 75 % o menos al inicio del combate, otorga Def/Res+7",
                new LowHpCondition(0.75),
                [
                    new StatChangeEffect(StatName.Def, 7),
                    new StatChangeEffect(StatName.Res, 7)
                ]),
            "resistance +5" => new Skill(
                "Resistance +5",
                "Otorga Res+5",
                new AlwaysTrueCondition(),
                [new StatChangeEffect(StatName.Res, 5)]
            ),
            "atk/def +5" => new Skill(
                "Atk/Def +5", 
                "Otorga Atk+5 y Def+5",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, 5),
                    new StatChangeEffect(StatName.Def, 5)
                ]),
            "atk/res +5" => new Skill(
                "Atk/Res +5", 
                "Otorga Atk+5 y Res+5",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, 5),
                    new StatChangeEffect(StatName.Res, 5)
                ]),
            "spd/res +5" => new Skill(
                "Spd/Res +5", 
                "Otorga Spd+5 y Res+5",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Spd, 5),
                    new StatChangeEffect(StatName.Res, 5)
                ]),
            "deadly blade" => new Skill(
                "Deadly blade",
                "Si la unidad inicia el combate con una espada, otorga Atk/Spd+8 durante el combate",
                new AndCondition(
                    [
                        new InitiatingCombatCondition(),
                        new UsingSpecificWeaponCondition(WeaponName.Sword)
                    ]),
                [
                    new StatChangeEffect(StatName.Atk, 8),
                    new StatChangeEffect(StatName.Spd, 8)
                ]), 
            "death blow" => new Skill(
                "Death blow",
                "Si la unidad inicia el combate, otorga Atk+8 durante el combate.",
                new InitiatingCombatCondition(),
                [new StatChangeEffect(StatName.Atk, 8)]
                ),
            "armored blow" => new Skill(
                "Armored blow",
                "Si la unidad inicia el combate, otorga Def+8 durante el combate.",
                new InitiatingCombatCondition(),
                [new StatChangeEffect(StatName.Def, 8)]
            ),
            "darting blow" => new Skill(
                "Darting blow",
                "Si la unidad inicia el combate, otorga Spd+8 durante el combate.",
                new InitiatingCombatCondition(),
                [new StatChangeEffect(StatName.Spd, 8)]
            ),
            "warding blow" => new Skill(
                "Warding blow",
                "Si la unidad inicia el combate, otorga Res+8 durante el combate.",
                new InitiatingCombatCondition(),
                [new StatChangeEffect(StatName.Res, 8)]
            ),
            "swift sparrow" => new Skill(
                "Swift Sparrow",
                " Si la unidad inicia el combate, otorga Atk/Spd+6 durante el combate.",
                new InitiatingCombatCondition(),
                [
                    new StatChangeEffect(StatName.Atk, 6),
                    new StatChangeEffect(StatName.Spd, 6)
                ]),
            "sturdy blow" => new Skill(
                "Sturdy Blow",
                "Si la unidad inicia el combate, otorga Atk/Def+6 durante el combate",
                new InitiatingCombatCondition(),
                [
                new StatChangeEffect(StatName.Atk, 6),
                new StatChangeEffect(StatName.Def, 6)
                ]),
            "mirror strike" => new Skill(
                "Mirror Strike",
                "Si la unidad inicia el combate, otorga Atk/Res+6 durante el combate",
                new InitiatingCombatCondition(),
                [
                new StatChangeEffect(StatName.Atk, 6),
                new StatChangeEffect(StatName.Res, 6)
                ]),
            "steady blow" => new Skill(
                "Steady Blow",
                "Si la unidad inicia el combate, otorga Spd/Def+6 durante el combate.",
                new InitiatingCombatCondition(),
                [
                new StatChangeEffect(StatName.Spd, 6),
                new StatChangeEffect(StatName.Def, 6)
                ]),
            "swift strike" => new Skill(
                "Swift Strike",
                "Si la unidad inicia el combate, otorga Spd/Res+6 durante el combate.",
                new InitiatingCombatCondition(),
                [
                new StatChangeEffect(StatName.Spd, 6),
                new StatChangeEffect(StatName.Res, 6)
                ]),
            "bracing blow" => new Skill(
                "Bracing Blow",
                "Si la unidad inicia el combate, otorga Def/Res+6 durante el combate.",
                new InitiatingCombatCondition(),
                [
                new StatChangeEffect(StatName.Def, 6),
                new StatChangeEffect(StatName.Res, 6)
                ]),
            "brazen atk/spd" => new Skill(
                "Brazen Atk/Spd",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Spd+10 durante el combate.",
                new LowHpCondition(0.8),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Spd, 10)
                ]),
            "brazen atk/def" => new Skill(
                "Brazen Atk/Def",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Def+10 durante el combate.",
                new LowHpCondition(0.8),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Def, 10)
                ]),
            "brazen atk/res" => new Skill(
                "Brazen Atk/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Res+10 durante el combate.",
                new LowHpCondition(0.8),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Res, 10)
                ]),
            "brazen spd/def" => new Skill(
                "Brazen Spd/Def",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Spd/Def+10 durante el combate.",
                new LowHpCondition(0.8),
                [
                    new StatChangeEffect(StatName.Spd, 10),
                    new StatChangeEffect(StatName.Def, 10)
                ]),
            "brazen spd/res" => new Skill(
                "Brazen Spd/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Spd/Res+10 durante el combate.",
                new LowHpCondition(0.8),
                [
                    new StatChangeEffect(StatName.Spd, 10),
                    new StatChangeEffect(StatName.Res, 10)
                ]),
            "brazen def/res" => new Skill(
                "Brazen Def/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Def/Res+10 durante el combate.",
                new LowHpCondition(0.8),
                [
                    new StatChangeEffect(StatName.Def, 10),
                    new StatChangeEffect(StatName.Res, 10)
                ]),
            "fire boost" => new Skill(
                "Fire Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Atk+6 durante el combate",
                new GreaterHpCondition(3),
                [new StatChangeEffect(StatName.Atk, 6)]
                ),
            "wind boost" => new Skill(
                "Wind Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Spd+6 durante el combate",
                new GreaterHpCondition(3),
                [new StatChangeEffect(StatName.Spd, 6)]
            ),
            "earth boost" => new Skill(
                "Eire Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Def+6 durante el combate",
                new GreaterHpCondition(3),
                [new StatChangeEffect(StatName.Def, 6)]
            ),
            "water boost" => new Skill(
                "Water Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, otorga Res+6 durante el combate",
                new GreaterHpCondition(3),
                [new StatChangeEffect(StatName.Res, 6)]
            ),
            "chaos style" => new Skill(
                "Chaos Style",
                "Si la unidad inicia el combate con un ataque fisico contra un rival armado con magia," +
                " o viceversa, otorga Spd+3 durante el combate.",
                new AndCondition(
                    [new OpposingWeaponTypeCondition(),
                    new InitiatingCombatCondition()]),
                [new StatChangeEffect(StatName.Spd, 3)]
                ),
            "blinding flash" => new Skill(
                "Blinding Flash", 
                "Si la unidad inicia el combate, inflige Spd-4 en el rival durante el combate",
                new InitiatingCombatCondition(),
                [new StatChangeEffect(StatName.Spd, -4, applyToOpponent: true)]
                ),
            "not *quite*" => new Skill(
                "Not *Quite*",
                "Si el rival inicia el combate, inflige Atk-4 en ese rival durante el combate",
                new InitiatingCombatCondition(rivalStarting: true),
                [new StatChangeEffect(StatName.Atk, -4, applyToOpponent: true )]
                ),
            "stunning smile" => new Skill(
                "Stunning Smile", 
                "Si el rival es hombre, inflige Spd-8 en ese rival durante el combate.",
                new IsMaleCondition(rivalGender: true),
                [new StatChangeEffect(StatName.Spd, -8, applyToOpponent: true)]
                ),
            "disarming sigh" => new Skill(
                "Disarming Sigh", 
                "Si el rival es hombre, inflige Atk-8 en ese rival durante el combate.",
                new IsMaleCondition(rivalGender: true),
                [new StatChangeEffect(StatName.Atk, -8, applyToOpponent: true)]
            ),
            "charmer" => new Skill(
                "Charming",
                "En un combate contra un rival que tambi \u0301en es el oponente mas " +
                "reciente de la unidad, inflige Atk/Spd-3 en ese rival durante el combate.",
                new SameOpponentCondition(),
                [
                    new StatChangeEffect(StatName.Atk, -3, applyToOpponent:true),
                    new StatChangeEffect(StatName.Spd, -3, applyToOpponent:true)
                ]
                ),
            "luna" => new Skill(
                "Luna",
                "Durante el primer ataque de la unidad, ignora la mitad de Def y Res base del rival. " +
                "(Considere esta reduccion como un Penalty",
                new AlwaysTrueCondition(),
                [new LunaPenaltyEffect()]
                ),
            "belief in love" => new Skill(
                "Belief in love",
                "Si el rival inicia el combate o tiene HP=100 % al inicio del combate, " +
                "inflige Atk/Def-5 en el rival durante el combate.",
                new OrCondition(
                    [
                        new InitiatingCombatCondition(rivalStarting: true), 
                        new HighHpCondition(1, rivalHp:true)
                    ]),
                [
                    new StatChangeEffect(StatName.Atk, -5, applyToOpponent:true),
                    new StatChangeEffect(StatName.Def, -5, applyToOpponent:true),
                ]),
            "beorc's blessing" => new Skill(
                "Beorc's Blessing",
                "Neutraliza los bonus del rival durante el combate.",
                new AlwaysTrueCondition(),
                [
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res),
                ]),
            "agnea's arrow" => new Skill(
                "Agnea's Arrow",
                "Neutraliza los penaltis de la unidad.",
                new AlwaysTrueCondition(),
                [
                    new StatPenaltyNeutralizeEffect(StatName.Atk),
                    new StatPenaltyNeutralizeEffect(StatName.Spd),
                    new StatPenaltyNeutralizeEffect(StatName.Def),
                    new StatPenaltyNeutralizeEffect(StatName.Res),
                ]
                ),
            "soulblade" => new Skill(
                "Soulblade",
                "Al atacar con una espada, el daño es calculado usando el promedio entre Def y Res " +
                "base del rival. Considere esto como un bonus o un penalty a los stats correspondientes. " +
                "Es decir, si la unidad ataca con una espada, otorga X Def e Y Res al rival. " +
                "Sea Z el promedio entre la Def y Res base del rival. Entonces X es Z " +
                "menos la Def base del rival e Y es Z menos la Res base del rival. " +
                "Dependiendo si X e Y son positivos o negativos, se consideraran bonus o penalties.",
                new UsingSpecificWeaponCondition(WeaponName.Sword),
                [new SoulbladeEffect()]
                ),
            "sandstorm" => new Skill(
                "Sandstorm",
                "Calcula el daño del Follow-Up utilizando el 150 % de la Def base de la unidad en vez del " +
                "Atk También considere esto como un Bonus o un Penalty de Atk. Es decir, esta habilidad otorga X Atk al " +
                "Follow-Up de la unidad. Sea Z igual a 1,5 por la Def base de la unidad. Entonces X es igual a Z " +
                "menos el Atk base de la unidad. Esto puede ser un bonus o un penalty dependiendo del signo de X.",
                new AlwaysTrueCondition(),
                [new SandstormEffect()]
                ),
            "sword agility" => new Skill(
                "Sword Agility",
                "Si la unidad usa espada, otorga Spd+12 a un costo de Atk-6 durante el combate.",
                new UsingSpecificWeaponCondition(WeaponName.Sword),
                [
                    new StatChangeEffect(StatName.Spd, 12),
                    new StatChangeEffect(StatName.Atk, -6)
                ]
                ),
            "lance power" => new Skill(
                "Lance Power",
                "Otorga Atk+10 a un costo de Def-10 al usar una lanza.",
                new UsingSpecificWeaponCondition(WeaponName.Lance),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Def, -10)
                ]),
            "sword power" => new Skill(
                "Sword Power",
                "Otorga Atk+10 a un costo de Def-10 al usar una espada.",
                new UsingSpecificWeaponCondition(WeaponName.Sword),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Def, -10)
                ]),
            "bow focus" => new Skill(
                "Bow Focus",
                "Otorga Atk+10 a un costo de Res-10 al usar un arco.",
                new UsingSpecificWeaponCondition(WeaponName.Bow),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Res, -10)
                ]),
            "lance agility" => new Skill(
                "Lance Agility",
                "Otorga Spd+12 a un costo de Atk-6 al usar una lanza.",
                new UsingSpecificWeaponCondition(WeaponName.Lance),
                [
                    new StatChangeEffect(StatName.Spd, 12),
                    new StatChangeEffect(StatName.Atk, -6)
                ]),
            "axe power" => new Skill(
                "Axe Power",
                "Otorga Atk+10 a un costo de Def-10 al usar una hacha.",
                new UsingSpecificWeaponCondition(WeaponName.Axe),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Def, -10)
                ]),
            "bow agility" => new Skill(
                "Bow Agility",
                "Otorga Spd+12 a un costo de Atk-6 al usar un arco.",
                new UsingSpecificWeaponCondition(WeaponName.Bow),
                [
                    new StatChangeEffect(StatName.Spd, 12),
                    new StatChangeEffect(StatName.Atk, -6)
                ]),
            "sword focus" => new Skill(
                "Sword Focus",
                "Otorga Atk+10 a un costo de Res-10 al usar una espada.",
                new UsingSpecificWeaponCondition(WeaponName.Sword),
                [
                    new StatChangeEffect(StatName.Atk, 10),
                    new StatChangeEffect(StatName.Res, -10)
                ]),
            "close def" => new Skill(
                "Close Def",
                "Si el rival inicia el combate usando espada, lanza o hacha," +
                " otorga Def/Res+8 y neutraliza los bonus del rival durante el combate.",
                new AndCondition(
                    [
                        new OrCondition(
                        [
                            new UsingSpecificWeaponCondition(WeaponName.Sword, opponentUsing: true),
                            new UsingSpecificWeaponCondition(WeaponName.Lance, opponentUsing: true),
                            new UsingSpecificWeaponCondition(WeaponName.Axe, opponentUsing: true),
                        ]),
                        new InitiatingCombatCondition(rivalStarting: true)
                    ]),
                [
                    new StatChangeEffect(StatName.Def, 8),
                    new StatChangeEffect(StatName.Res, 8),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res),
                ]
                ),
            "distant def" => new Skill(
                "Distant Def",
                "Si el rival inicia el combate usando magia o arco," +
                " otorga Def/Res+8 y neutraliza los bonus del rival durante el combate.",
                new AndCondition(
                [
                    new OrCondition(
                    [
                        new UsingSpecificWeaponCondition(WeaponName.Magic, opponentUsing: true),
                        new UsingSpecificWeaponCondition(WeaponName.Bow, opponentUsing: true),
                    ]),
                    new InitiatingCombatCondition(rivalStarting: true)
                ]),
                [
                    new StatChangeEffect(StatName.Def, 8),
                    new StatChangeEffect(StatName.Res, 8),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res),
                ]
            ),
            "lull atk/spd" => new Skill(
                "Lull Atk/Spd",
                "Inflige Atk/Spd-3 en el rival y neutraliza los bonus a Atk/Spd del combate.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, -3, applyToOpponent: true),
                    new StatChangeEffect(StatName.Spd, -3, applyToOpponent: true),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                ]),
            "lull atk/def" => new Skill(
                "Lull Atk/Def",
                "Inflige Atk/Def-3 en el rival y neutraliza los bonus a Atk/Def del combate.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, -3, applyToOpponent: true),
                    new StatChangeEffect(StatName.Def, -3, applyToOpponent: true),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Def),
                ]),
            "lull atk/res" => new Skill(
                "Lull Atk/Res",
                "Inflige Atk/Res-3 en el rival y neutraliza los bonus a Atk/Res del combate.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, -3, applyToOpponent: true),
                    new StatChangeEffect(StatName.Res, -3, applyToOpponent: true),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Res),
                ]),
            "lull spd/def" => new Skill(
                "Lull Spd/Def",
                "Inflige Spd/Def-3 en el rival y neutraliza los bonus a Spd/Def del combate.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Spd, -3, applyToOpponent: true),
                    new StatChangeEffect(StatName.Def, -3, applyToOpponent: true),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                ]),
            "lull spd/res" => new Skill(
                "Lull Spd/Res",
                "Inflige Spd/Res-3 en el rival y neutraliza los bonus a Spd/Res del combate.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Spd, -3, applyToOpponent: true),
                    new StatChangeEffect(StatName.Res, -3, applyToOpponent: true),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Res),
                ]),
            "lull def/res" => new Skill(
                "Lull Def/Res",
                "Inflige Def/Res-3 en el rival y neutraliza los bonus a Def/Res del combate.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Def, -3, applyToOpponent: true),
                    new StatChangeEffect(StatName.Res, -3, applyToOpponent: true),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res),
                ]),
            "fort. def/res" => new Skill(
                "Fort. Def/Res",
                "Otorga Def/Res+6. Inflige Atk-2 a si mismo.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Def, 6),
                    new StatChangeEffect(StatName.Res, 6),
                    new StatChangeEffect(StatName.Atk, -2)
                ]),
            "life and death" => new Skill(
                "Life and Death",
                "Otorga Atk/Spd+6. Inflige Def/Res-5 a si mismo.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, 6),
                    new StatChangeEffect(StatName.Spd, 6),
                    new StatChangeEffect(StatName.Def, -5),
                    new StatChangeEffect(StatName.Res, -5)
                ]),
            "solid ground" => new Skill(
                "Solid Ground",
                "Otorga Atk/Def+6. Inflige Res-5 a si mismo.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, 6),
                    new StatChangeEffect(StatName.Def, 6),
                    new StatChangeEffect(StatName.Res, -5)
                ]),
            "still water" => new Skill(
                "Still Water",
                "Otorga Atk/Res+6. Inflige Def-5 a si mismo.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, 6),
                    new StatChangeEffect(StatName.Res, 6),
                    new StatChangeEffect(StatName.Def, -5)
                ]),
            "dragonskin" => new Skill(
                "Dragonskin",
                "Si el rival inicia el combate o si el HP del rival >= 75% al inicio del combate, " +
                "otorga Atk/Spd/Def/Res+6 a la unidad durante el combate y neutraliza los bonus del rival.",
                new OrCondition(
                    [
                        new InitiatingCombatCondition(rivalStarting: true),
                        new HighHpCondition(0.75, rivalHp: true)
                    ]
                    ),
                [
                    new StatChangeEffect(StatName.Atk, 6),
                    new StatChangeEffect(StatName.Spd, 6),
                    new StatChangeEffect(StatName.Def, 6),
                    new StatChangeEffect(StatName.Res, 6),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res)
                
                ]),
            "light and dark" => new Skill(
                "Light and Dark",
                "Inflige Atk/Spd/Def/Res-5 en el rival, neutraliza los penaltis de la unidad y los bonus del rival.",
                new AlwaysTrueCondition(),
                [
                    new StatChangeEffect(StatName.Atk, -5, applyToOpponent: true),
                    new StatChangeEffect(StatName.Spd, -5, applyToOpponent: true),
                    new StatChangeEffect(StatName.Def, -5, applyToOpponent: true),
                    new StatChangeEffect(StatName.Res, -5, applyToOpponent: true),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res),
                    new StatPenaltyNeutralizeEffect(StatName.Atk),
                    new StatPenaltyNeutralizeEffect(StatName.Spd),
                    new StatPenaltyNeutralizeEffect(StatName.Def),
                    new StatPenaltyNeutralizeEffect(StatName.Res),
                ]),
            
            
            
            
            
            _ => new UnimplementedSkill(skillName)
        };
        
        
    }
}