using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Exceptions;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Models.Skills.Conditions;
using Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;
using Fire_Emblem.Models.Skills.Conditions.WithBonusesConditions;
using Fire_Emblem.Models.Skills.Effects.BaseStatEffects;
using Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.SkillsManager;

public class SkillFactory
{
    public ISkill GetSkill(string skillName)
    {
        return skillName.ToLower() switch
        {
            "hp +15" => new OneTimeSkill(
                "HP +15", 
                "Otorga max HP+15 (esta habilidad no requiere ser anunciada).",
                new OneTimeCondition(),
                new EffectsList([new MaxHpBoostEffect(15)])
            ),
            "fair fight" => new BasicSkill(
                "Fair Fight",
                "Si la unidad inicia el combate, otorga Atk+6 a la unidad y al rival durante el combate",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 6),
                                new StatBoostOpponentEffect(StatName.Atk, 6)])
            ),
            "will to win" => new BasicSkill(
                "Will to Win",
                "Si el HP de la unidad está al 50% o menos al inicio del combate, " +
                "otorga Atk+8 durante el combate.",
                new LowHpCondition(0.5),
                new EffectsList([new StatBoostEffect(StatName.Atk, 8)])
            ),
            "single-minded" => new BasicSkill(
                "Single-Minded",
                "En un combate contra un rival que también es el oponente más reciente de la unidad, " +
                "otorga Atk+8 durante el combate.",
                new SameOpponentCondition(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 8)])
            ),
            "ignis" => new BasicSkill(
                "Ignis",
                "Otorga Atk+50% al primer ataque de la unidad.",
                new FirstAttackCondition(),
                new EffectsList([new FirstAttackBoostEffect(50)])
            ),
            "perceptive" => new BasicSkill(
                "Perceptive",
                "Si la unidad inicia el combate, otorga Spd+12 a la unidad durante el combate, " +
                "y por cada cuatro puntos de Spd (sin contar bonus o penalty), la unidad gana Spd+1 adicional.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Spd, 12),
                                new ScalingStatBoostEffect(StatName.Spd, 4)])
            ),
            "tome precision" => new BasicSkill(
                "Tome Precision",
                "Otorga Atk/Spd+6 al usar magia.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Magic),
                new EffectsList([new StatBoostEffect(StatName.Atk, 6),
                                new StatBoostEffect(StatName.Spd, 6)])
            ),
            "attack +6" => new BasicSkill(
                "Attack +6",
                "Otorga Atk+6",
                new AlwaysTrueCondition(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 6)])
            ),
            "speed +5" => new BasicSkill(
                "Speed +5",
                "Otorga Spd+5",
                new AlwaysTrueCondition(),
                new EffectsList([new StatBoostEffect(StatName.Spd, 5)])
            ),
            "defense +5" => new BasicSkill(
                "Defense +5",
                "Otorga Def+5",
                new AlwaysTrueCondition(),
                new EffectsList([new StatBoostEffect(StatName.Def, 5)])
            ),
            "wrath" => new BasicSkill(
                "Wrath",
                "Al inicio del combate, por cada punto de HP que la unidad ha perdido, " +
                "otorga Atk/Spd+1 durante el combate. (Max +30)",
                new AlwaysTrueCondition(),
                new EffectsList([new WrathEffect()])
            ),
            "resolve" => new BasicSkill(
                "Resolve",
                "Si el HP de la unidad es <= a al 75 % o menos al inicio del combate, otorga Def/Res+7",
                new LowHpCondition(0.75),
                new EffectsList([new StatBoostEffect(StatName.Def, 7),
                                new StatBoostEffect(StatName.Res, 7)])
            ),
            "resistance +5" => new BasicSkill(
                "Resistance +5",
                "Otorga Res+5",
                new AlwaysTrueCondition(),
                new EffectsList([new StatBoostEffect(StatName.Res, 5)])
            ),
            "atk/def +5" => new BasicSkill(
                "Atk/Def +5", 
                "Otorga Atk+5 y Def+5",
                new AlwaysTrueCondition(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 5),
                                new StatBoostEffect(StatName.Def, 5)])
            ),
            "atk/res +5" => new BasicSkill(
                "Atk/Res +5", 
                "Otorga Atk+5 y Res+5",
                new AlwaysTrueCondition(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 5),
                                new StatBoostEffect(StatName.Res, 5)])
            ),
            "spd/res +5" => new BasicSkill(
                "Spd/Res +5", 
                "Otorga Spd+5 y Res+5",
                new AlwaysTrueCondition(),
                new EffectsList([new StatBoostEffect(StatName.Spd, 5),
                                new StatBoostEffect(StatName.Res, 5)])
            ),
            "deadly blade" => new BasicSkill(
                "Deadly Blade",
                "Si la unidad inicia el combate con una espada, otorga Atk/Spd+8 durante el combate",
                new AndCondition(
                    new ConditionsList([
                        new InitiatingCombatConditionSelf(),
                        new UsingSpecificWeaponConditionSelf(WeaponName.Sword)
                    ])
                ),
                new EffectsList([new StatBoostEffect(StatName.Atk, 8),
                                new StatBoostEffect(StatName.Spd, 8)])
            ),
            "death blow" => new BasicSkill(
                "Death Blow",
                "Si la unidad inicia el combate, otorga Atk+8 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 8)])
            ),
            "armored blow" => new BasicSkill(
                "Armored Blow",
                "Si la unidad inicia el combate, otorga Def+8 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Def, 8)])
            ),
            "darting blow" => new BasicSkill(
                "Darting Blow",
                "Si la unidad inicia el combate, otorga Spd+8 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Spd, 8)])
            ),
            "warding blow" => new BasicSkill(
                "Warding Blow",
                "Si la unidad inicia el combate, otorga Res+8 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Res, 8)])
            ),
            "swift sparrow" => new BasicSkill(
                "Swift Sparrow",
                "Si la unidad inicia el combate, otorga Atk/Spd+6 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 6),
                                new StatBoostEffect(StatName.Spd, 6)])
            ),
            "sturdy blow" => new BasicSkill(
                "Sturdy Blow",
                "Si la unidad inicia el combate, otorga Atk/Def+6 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 6),
                                new StatBoostEffect(StatName.Def, 6)])
            ),
            "mirror strike" => new BasicSkill(
                "Mirror Strike",
                "Si la unidad inicia el combate, otorga Atk/Res+6 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Atk, 6),
                                new StatBoostEffect(StatName.Res, 6)])
            ),
            "steady blow" => new BasicSkill(
                "Steady Blow",
                "Si la unidad inicia el combate, otorga Spd/Def+6 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Spd, 6),
                                new StatBoostEffect(StatName.Def, 6)])
            ),
            "swift strike" => new BasicSkill(
                "Swift Strike",
                "Si la unidad inicia el combate, otorga Spd/Res+6 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Spd, 6),
                                new StatBoostEffect(StatName.Res, 6)])
            ),
            "bracing blow" => new BasicSkill(
                "Bracing Blow",
                "Si la unidad inicia el combate, otorga Def/Res+6 durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatBoostEffect(StatName.Def, 6),
                                new StatBoostEffect(StatName.Res, 6)])
            ),
            "brazen atk/spd" => new BasicSkill(
                "Brazen Atk/Spd",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Spd+10 durante el combate.",
                new LowHpCondition(0.8),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatBoostEffect(StatName.Spd, 10)
                ])
            ),
            "brazen atk/def" => new BasicSkill(
                "Brazen Atk/Def",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Def+10 durante el combate.",
                new LowHpCondition(0.8),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatBoostEffect(StatName.Def, 10)
                ])
            ),
            "brazen atk/res" => new BasicSkill(
                "Brazen Atk/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Atk/Res+10 durante el combate.",
                new LowHpCondition(0.8),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatBoostEffect(StatName.Res, 10)
                ])
            ),
            "brazen spd/def" => new BasicSkill(
                "Brazen Spd/Def",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Spd/Def+10 durante el combate.",
                new LowHpCondition(0.8),
                new EffectsList([
                    new StatBoostEffect(StatName.Spd, 10),
                    new StatBoostEffect(StatName.Def, 10)
                ])
            ),
            "brazen spd/res" => new BasicSkill(
                "Brazen Spd/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Spd/Res+10 durante el combate.",
                new LowHpCondition(0.8),
                new EffectsList([
                    new StatBoostEffect(StatName.Spd, 10),
                    new StatBoostEffect(StatName.Res, 10)
                ])
            ),
            "brazen def/res" => new BasicSkill(
                "Brazen Def/Res",
                "Al inicio del combate, si el HP de la unidad <= 80 %, otorga Def/Res+10 durante el combate.",
                new LowHpCondition(0.8),
                new EffectsList([
                    new StatBoostEffect(StatName.Def, 10),
                    new StatBoostEffect(StatName.Res, 10)
                ])
            ),
            "fire boost" => new BasicSkill(
                "Fire Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, " +
                "otorga Atk+6 durante el combate.",
                new GreaterHpCondition(3),
                new EffectsList([new StatBoostEffect(StatName.Atk, 6)])
            ),
            "wind boost" => new BasicSkill(
                "Wind Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, " +
                "otorga Spd+6 durante el combate.",
                new GreaterHpCondition(3),
                new EffectsList([new StatBoostEffect(StatName.Spd, 6)])
            ),
            "earth boost" => new BasicSkill(
                "Earth Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3, " +
                "otorga Def+6 durante el combate.",
                new GreaterHpCondition(3),
                new EffectsList([new StatBoostEffect(StatName.Def, 6)])
            ),
            "water boost" => new BasicSkill(
                "Water Boost",
                "Al inicio del combate, si el HP de la unidad >= HP del rival+3," +
                " otorga Res+6 durante el combate.",
                new GreaterHpCondition(3),
                new EffectsList([new StatBoostEffect(StatName.Res, 6)])
            ),
            "chaos style" => new BasicSkill(
                "Chaos Style",
                "Si la unidad inicia el combate con un ataque físico contra un rival armado con magia, " +
                "o viceversa, otorga Spd+3 durante el combate.",
                new AndCondition(
                    new ConditionsList([
                        new OpposingWeaponTypeCondition(),
                        new InitiatingCombatConditionSelf()
                    ])
                ),
                new EffectsList([new StatBoostEffect(StatName.Spd, 3)])
            ),
            "blinding flash" => new BasicSkill(
                "Blinding Flash", 
                "Si la unidad inicia el combate, inflige Spd-4 en el rival durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new StatPenaltyOpponentEffect(StatName.Spd, 4)])
            ),
            "not *quite*" => new BasicSkill(
                "Not *Quite*",
                "Si el rival inicia el combate, inflige Atk-4 en ese rival durante el combate.",
                new InitiatingCombatConditionRival(),
                new EffectsList([new StatPenaltyOpponentEffect(StatName.Atk, 4)])
            ),
            "stunning smile" => new BasicSkill(
                "Stunning Smile", 
                "Si el rival es hombre, inflige Spd-8 en ese rival durante el combate.",
                new IsMaleConditionRival(),
                new EffectsList([new StatPenaltyOpponentEffect(StatName.Spd, 8)])
            ),
            "disarming sigh" => new BasicSkill(
                "Disarming Sigh", 
                "Si el rival es hombre, inflige Atk-8 en ese rival durante el combate.",
                new IsMaleConditionRival(),
                new EffectsList([new StatPenaltyOpponentEffect(StatName.Atk, 8)])
            ),
            "charmer" => new BasicSkill(
                "Charmer",
                "En un combate contra un rival que también es el oponente más reciente de la unidad, " +
                "inflige Atk/Spd-3 en ese rival durante el combate.",
                new SameOpponentCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Atk, 3),
                    new StatPenaltyOpponentEffect(StatName.Spd, 3)
                ])
            ),
            "luna" => new BasicSkill(
                "Luna",
                "Durante el primer ataque de la unidad, ignora la mitad de Def y Res base del rival. " +
                "(Considere esta reducción como un Penalty).",
                new AlwaysTrueCondition(),
                new EffectsList([new LunaPenaltyEffect()])
            ),
            "belief in love" => new BasicSkill(
                "Belief in Love",
                "Si el rival inicia el combate o tiene HP=100 % al inicio del combate, " +
                "inflige Atk/Def-5 en el rival durante el combate.",
                new OrCondition(
                    new ConditionsList([
                        new InitiatingCombatConditionRival(), 
                        new HighHpConditionRival(1)
                    ])
                ),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Atk, 5),
                    new StatPenaltyOpponentEffect(StatName.Def, 5)
                ])
            ),
            "beorc's blessing" => new BasicSkill(
                "Beorc's Blessing",
                "Neutraliza los bonus del rival durante el combate.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res)
                ])
            ),
            "agnea's arrow" => new BasicSkill(
                "Agnea's Arrow",
                "Neutraliza los penaltis de la unidad.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyNeutralizeEffect(StatName.Atk),
                    new StatPenaltyNeutralizeEffect(StatName.Spd),
                    new StatPenaltyNeutralizeEffect(StatName.Def),
                    new StatPenaltyNeutralizeEffect(StatName.Res)
                ])
            ),
            "soulblade" => new BasicSkill(
                "Soulblade",
                "Al atacar con una espada, el daño es calculado usando el promedio entre Def y Res " +
                "base del rival. Considere esto como un bonus o un penalty a los stats correspondientes.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Sword),
                new EffectsList([new SoulbladeEffect()])
            ),
            "sandstorm" => new BasicSkill(
                "Sandstorm",
                "Calcula el daño del Follow-Up utilizando el 150 % de la Def base de la unidad en " +
                "vez del Atk. Considere esto como un Bonus o un Penalty de Atk.",
                new AlwaysTrueCondition(),
                new EffectsList([new SandstormEffect()])
            ),
            "sword agility" => new BasicSkill(
                "Sword Agility",
                "Si la unidad usa espada, otorga Spd+12 a un costo de Atk-6 durante el combate.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Sword),
                new EffectsList([
                    new StatBoostEffect(StatName.Spd, 12),
                    new StatPenaltyEffect(StatName.Atk, 6)
                ])
            ),
            "lance power" => new BasicSkill(
                "Lance Power",
                "Otorga Atk+10 a un costo de Def-10 al usar una lanza.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Lance),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatPenaltyEffect(StatName.Def, 10)
                ])
            ),
            "sword power" => new BasicSkill(
                "Sword Power",
                "Otorga Atk+10 a un costo de Def-10 al usar una espada.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Sword),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatPenaltyEffect(StatName.Def, 10)
                ])
            ),
            "bow focus" => new BasicSkill(
                "Bow Focus",
                "Otorga Atk+10 a un costo de Res-10 al usar un arco.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Bow),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatPenaltyEffect(StatName.Res, 10)
                ])
            ),
            "lance agility" => new BasicSkill(
                "Lance Agility",
                "Otorga Spd+12 a un costo de Atk-6 al usar una lanza.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Lance),
                new EffectsList([
                    new StatBoostEffect(StatName.Spd, 12),
                    new StatPenaltyEffect(StatName.Atk, 6)
                ])
            ),
            "axe power" => new BasicSkill(
                "Axe Power",
                "Otorga Atk+10 a un costo de Def-10 al usar una hacha.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Axe),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatPenaltyEffect(StatName.Def, 10)
                ])
            ),
            "bow agility" => new BasicSkill(
                "Bow Agility",
                "Otorga Spd+12 a un costo de Atk-6 al usar un arco.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Bow),
                new EffectsList([
                    new StatBoostEffect(StatName.Spd, 12),
                    new StatPenaltyEffect(StatName.Atk, 6)
                ])
            ),
            "sword focus" => new BasicSkill(
                "Sword Focus",
                "Otorga Atk+10 a un costo de Res-10 al usar una espada.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Sword),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 10),
                    new StatPenaltyEffect(StatName.Res, 10)
                ])
            ),
            "close def" => new BasicSkill(
                "Close Def",
                "Si el rival inicia el combate usando espada, lanza o hacha, " +
                "otorga Def/Res+8 y neutraliza los bonus del rival durante el combate.",
                new AndCondition(
                    new ConditionsList([
                        new OrCondition(
                            new ConditionsList([
                                new UsingSpecificWeaponConditionRival(WeaponName.Sword),
                                new UsingSpecificWeaponConditionRival(WeaponName.Lance),
                                new UsingSpecificWeaponConditionRival(WeaponName.Axe)
                            ])
                        ),
                        new InitiatingCombatConditionRival()
                    ])
                ),
                new EffectsList([
                    new StatBoostEffect(StatName.Def, 8),
                    new StatBoostEffect(StatName.Res, 8),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res)
                ])
            ),
            "distant def" => new BasicSkill(
                "Distant Def",
                "Si el rival inicia el combate usando magia o arco, " +
                "otorga Def/Res+8 y neutraliza los bonus del rival durante el combate.",
                new AndCondition(
                    new ConditionsList([
                        new OrCondition(
                            new ConditionsList([
                                new UsingSpecificWeaponConditionRival(WeaponName.Magic),
                                new UsingSpecificWeaponConditionRival(WeaponName.Bow)
                            ])
                        ),
                        new InitiatingCombatConditionRival()
                    ])
                ),
                new EffectsList([
                    new StatBoostEffect(StatName.Def, 8),
                    new StatBoostEffect(StatName.Res, 8),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res)
                ])
            ),
            "lull atk/spd" => new BasicSkill(
                "Lull Atk/Spd",
                "Inflige Atk/Spd-3 en el rival y neutraliza los bonus a Atk/Spd del combate.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Atk, 3),
                    new StatPenaltyOpponentEffect(StatName.Spd, 3),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd)
                ])
            ),
            "lull atk/def" => new BasicSkill(
                "Lull Atk/Def",
                "Inflige Atk/Def-3 en el rival y neutraliza los bonus a Atk/Def del combate.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Atk, 3),
                    new StatPenaltyOpponentEffect(StatName.Def, 3),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Def)
                ])
            ),
            "lull atk/res" => new BasicSkill(
                "Lull Atk/Res",
                "Inflige Atk/Res-3 en el rival y neutraliza los bonus a Atk/Res del combate.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Atk, 3),
                    new StatPenaltyOpponentEffect(StatName.Res, 3),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Res)
                ])
            ),
            "lull spd/def" => new BasicSkill(
                "Lull Spd/Def",
                "Inflige Spd/Def-3 en el rival y neutraliza los bonus a Spd/Def del combate.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Spd, 3),
                    new StatPenaltyOpponentEffect(StatName.Def, 3),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def)
                ])
            ),
            "lull spd/res" => new BasicSkill(
                "Lull Spd/Res",
                "Inflige Spd/Res-3 en el rival y neutraliza los bonus a Spd/Res del combate.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Spd, 3),
                    new StatPenaltyOpponentEffect(StatName.Res, 3),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Res)
                ])
            ),
            "lull def/res" => new BasicSkill(
                "Lull Def/Res",
                "Inflige Def/Res -3 en el rival y neutraliza los bonus a Def/Res del combate.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Def, 3),
                    new StatPenaltyOpponentEffect(StatName.Res, 3),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res)
                ])
            ),
            "fort. def/res" => new BasicSkill(
                "Fort. Def/Res",
                "Otorga Def/Res+6. Inflige Atk-2 a sí mismo.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatBoostEffect(StatName.Def, 6),
                    new StatBoostEffect(StatName.Res, 6),
                    new StatPenaltyEffect(StatName.Atk, 2)
                ])
            ),
            "life and death" => new BasicSkill(
                "Life and Death",
                "Otorga Atk/Spd+6. Inflige Def/Res-5 a sí mismo.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 6),
                    new StatBoostEffect(StatName.Spd, 6),
                    new StatPenaltyEffect(StatName.Def, 5),
                    new StatPenaltyEffect(StatName.Res, 5)
                ])
            ),
            "solid ground" => new BasicSkill(
                "Solid Ground",
                "Otorga Atk/Def+6. Inflige Res-5 a sí mismo.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 6),
                    new StatBoostEffect(StatName.Def, 6),
                    new StatPenaltyEffect(StatName.Res, 5)
                ])
            ),
            "still water" => new BasicSkill(
                "Still Water",
                "Otorga Atk/Res+6. Inflige Def-5 a sí mismo.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 6),
                    new StatBoostEffect(StatName.Res, 6),
                    new StatPenaltyEffect(StatName.Def, 5)
                ])
            ),
            "dragonskin" => new BasicSkill(
                "Dragonskin",
                "Si el rival inicia el combate o si el HP del rival >= 75% al inicio del combate, " +
                "otorga Atk/Spd/Def/Res+6 a la unidad durante el combate y neutraliza los bonus del rival.",
                new OrCondition(
                    new ConditionsList([
                        new InitiatingCombatConditionRival(),
                        new HighHpConditionRival(0.75)
                    ])
                ),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 6),
                    new StatBoostEffect(StatName.Spd, 6),
                    new StatBoostEffect(StatName.Def, 6),
                    new StatBoostEffect(StatName.Res, 6),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res)
                ])
            ),
            "light and dark" => new BasicSkill(
                "Light and Dark",
                "Inflige Atk/Spd/Def/Res-5 en el rival, neutraliza los penaltis de la unidad y " +
                "los bonus del rival.",
                new AlwaysTrueCondition(),
                new EffectsList([
                    new StatPenaltyOpponentEffect(StatName.Atk, 5),
                    new StatPenaltyOpponentEffect(StatName.Spd, 5),
                    new StatPenaltyOpponentEffect(StatName.Def, 5),
                    new StatPenaltyOpponentEffect(StatName.Res, 5),
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Res),
                    new StatPenaltyNeutralizeEffect(StatName.Atk),
                    new StatPenaltyNeutralizeEffect(StatName.Spd),
                    new StatPenaltyNeutralizeEffect(StatName.Def),
                    new StatPenaltyNeutralizeEffect(StatName.Res)
                ])
            ),
            "dragon wall" => new SecondDegreeSkill(
                "Dragon Wall",
                "Si Res de la unidad > Res del rival, reduce el dano de cada ataque del rival por un " +
                "porcentaje=diferencia entre los stats x 4 (ma \u0301x. 40 %).",
                new StatComparisonCondition(StatName.Res, StatName.Res),
                new EffectsList([new ScalingStatDamageReductionEffect(StatName.Res, 40)])
                ),
            "dodge" => new SecondDegreeSkill(
                "Dodge", 
                "Si Spd de la unidad > Spd del rival, reduce el dano de cada ataque del rival por un " +
                "porcentaje=diferencia entre los stats x 4 (max. 40 %)",
                new StatComparisonCondition(StatName.Spd, StatName.Spd),
                new EffectsList([new ScalingStatDamageReductionEffect(StatName.Spd, 40)])
                ),
            "golden lotus" => new BasicSkill(
                "Golden Lotus",
                "Reduce el daño del primer ataque del rival en un 50 % si este hace daño físico.",
                new UsingPhysicalWeaponConditionRival(),
                new EffectsList([new FirstAttackPercentReductionEffect(0.5)])
                ),
            "gentility" => new BasicSkill(
                "Gentility", 
                "La unidad recibe -5 de daño en cada ataque del rival",
                new AlwaysTrueCondition(),
                new EffectsList([new FlatDamageReductionEffect(5)])
                ),
            "bow guard" => new BasicSkill(
                "Bow Guard",
                "Si el rival usa un arco, la unidad recibe -5 daño en cada ataque del rival",
                new UsingSpecificWeaponConditionRival(WeaponName.Bow),
                new EffectsList([new FlatDamageReductionEffect(5)])
                ),
            "arms shield" => new BasicSkill(
                "Arms Shield", 
                "Si la unidad tiene ventaja de arma, la unidad recibe -7 de daño en cada ataque del rival.",
                new TriangleAdvantageCondition(),
                new EffectsList([new FlatDamageReductionEffect(7)])
                    ),
            "axe guard" => new BasicSkill(
                "Axe Guard", 
                "Si el rival usa hachas, la unidad recibe -5 daño en cada ataque del rival",
                new UsingSpecificWeaponConditionRival(WeaponName.Axe),
                new EffectsList([new FlatDamageReductionEffect(5)])
                ),
            "magic guard" => new BasicSkill(
                "Magic Guard", 
                "Si el rival usa magia, la unidad recibe -5 daño en cada ataque del rival",
                new UsingSpecificWeaponConditionRival(WeaponName.Magic),
                new EffectsList([new FlatDamageReductionEffect(5)])
            ),
            "lance guard" => new BasicSkill(
                "Lange Guard", 
                "Si el rival usa lanza, la unidad recibe -5 daño en cada ataque del rival",
                new UsingSpecificWeaponConditionRival(WeaponName.Lance),
                new EffectsList([new FlatDamageReductionEffect(5)])
            ),
            "sympathetic" => new BasicSkill(
                "Sympathetic", 
                "Si el rival inicia el combate y el HP de la unidad está" +
                "  al 50 % o menos al inicio del combate, la unidad recibe -5 daño en cada ataque del rival.",
                new AndCondition(
                    new ConditionsList(
                        [
                            new InitiatingCombatConditionRival(),
                            new LowHpCondition(0.5)
                        ])),
                new EffectsList([new FlatDamageReductionEffect(5)])
                ),
            "back at you" => new BasicSkill(
                "Back At You", 
                "Si el rival inicia el combate, la unidad inflige daño extra en cada ataque = mitad del HP " +
                "que la unidad ha perdido (considera solo el HP perdido hasta el combate anterior).",
                new InitiatingCombatConditionRival(),
                new EffectsList([new LostHpExtraFlatDamageEffect(0.5)])
                ),
            "lunar brace" => new SecondDegreeSkill(
                "Lunar Brace",
                "Si la unidad inicia el combate con un ataque físico, inflige daño" +
                " extra=30 % de la Def del rival en cada ataque.",
                new AndCondition(
                    new ConditionsList([
                        new InitiatingCombatConditionSelf(),
                        new UsingPhysicalWeaponConditionSelf()
                    ])),
                new EffectsList([new LunarBraceEffect(0.3)])
                ),
            "bravery" => new BasicSkill(
                "Bravery",
                "La unidad inflige +5 de daño en cada ataque.",
                new AlwaysTrueCondition(),
                new EffectsList([new FlatAttackIncrementEffect(5)])
                ),
            "bushido" => new CompositeSkill(
                "Bushido",
                " Inflige +7 de daño por ataque. Si el Spd de la unidad > Spd del rival, reduce el daño " +
                "de los ataques del rival durante el combate por " +
                "un porcentaje = diferencia entre stats x 4 (ma. 40 %).",
                    new SkillList([
                        new BasicSkill(new AlwaysTrueCondition(), 
                            new EffectsList([new FlatAttackIncrementEffect(7)])),
                        new SecondDegreeSkill(new StatComparisonCondition(StatName.Spd, StatName.Spd), 
                            new EffectsList([new ScalingStatDamageReductionEffect(StatName.Spd,40)]))
                   ])
                ),
            "moon-twin wing" => new CompositeSkill(
                "Moon-Twin Wing",
                "Moon-Twin Wing: Al inicio del combate, si el HP de la unidad \u2265 25%, " +
                "inflige Atk/Spd-5 en el rival durante el combate, y también, si la Spd de la unidad > Spd del rival, " +
                "reduce el daño de los ataques del rival durante el combate en un porcentaje = " +
                "diferencia entre los stats x 4 (ma \u0301x. 40 %).",
                new SkillList(
                    [
                    new BasicSkill(
                        new HighHpCondition(0.25), 
                        new EffectsList([new StatPenaltyOpponentEffect(StatName.Atk, 5),
                            new StatPenaltyOpponentEffect(StatName.Spd, 5)])
                        ),
                    new SecondDegreeSkill(
                        new AndCondition(
                            new ConditionsList(
                                [
                                    new HighHpCondition(0.25), 
                                    new StatComparisonCondition(StatName.Spd, StatName.Spd)
                                ])),
                        new EffectsList([new ScalingStatDamageReductionEffect(StatName.Spd,40)]))])
                ),
            "blue skies" => new BasicSkill(
                "Blue Skies",
                "La unidad recibe -5 de dan \u0303o en cada ataque del rival " +
                "e inflige +5 de dan \u0303o en cada ataque.",
                new AlwaysTrueCondition(),
                new EffectsList(
                    [
                        new FlatDamageReductionEffect(5),
                        new FlatAttackIncrementEffect(5)
                    ])
                ),
            "aegis shield" => new BasicSkill(
                "Aegis shield",
                "Otorga Def+6 y Res+3. Reduce el dan \u0303o a la mitad en el primer ataque del rival",
                new AlwaysTrueCondition(),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Def, 6),
                        new StatBoostEffect(StatName.Res, 3),
                        new FirstAttackPercentReductionEffect(0.5)
                    ])
                ),
            "remote sparrow" => new BasicSkill(
                "Remote Sparrow",
                "Si la unidad inicia el combate, otorga Atk/Spd+7 y reduce el daño " +
                "del primer ataque del rival en un 30 %.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 7),
                    new StatBoostEffect(StatName.Spd, 7),
                    new FirstAttackPercentReductionEffect(0.3)])
                ),
            "remote mirror" => new BasicSkill(
                "Remote Mirror",
                "Si la unidad inicia el combate, otorga Atk/Spd+7 y reduce el daño " +
                "del primer ataque del rival en un 30 %.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 7),
                    new StatBoostEffect(StatName.Res, 10),
                    new FirstAttackPercentReductionEffect(0.3)])
            ),
            "remote sturdy" => new BasicSkill(
                "Remote sturdy",
                "Si la unidad inicia el combate, otorga Atk/Deg+7 y reduce el daño " +
                "del primer ataque del rival en un 30 %.",
                new InitiatingCombatConditionSelf(),
                new EffectsList([
                    new StatBoostEffect(StatName.Atk, 7),
                    new StatBoostEffect(StatName.Def, 10),
                    new FirstAttackPercentReductionEffect(0.3)])
            ),
            "fierce stance" => new BasicSkill(
                "Fierce Stance",
                "Si el rival inicia el combate, otorga Atk+8 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Atk, 8),
                        new FollowupAttackPercentReductionEffect(0.1)
                    ])),
            "darting stance" => new BasicSkill(
                "Darting Stance",
                "Si el rival inicia el combate, otorga Spd+8 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Spd, 8),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "steady stance" => new BasicSkill(
                "Steady Stance",
                "Si el rival inicia el combate, otorga Def+8 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                            new EffectsList([
                                    new StatBoostEffect(StatName.Def, 8),
                                    new FollowupAttackPercentReductionEffect(0.1)

                                ])
                ),
            "warding stance" => new BasicSkill(
                "Warding Stance",
                "Si el rival inicia el combate, otorga Res+8 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Res, 8),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "kestrel stance" => new BasicSkill(
                "Kestrel Stance",
                "Si el rival inicia el combate, otorga Atk/Spd+6 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Atk, 6),
                    new StatBoostEffect(StatName.Spd, 6),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "sturdy stance" => new BasicSkill(
                "Sturdy Stance",
                "Si el rival inicia el combate, otorga Atk/Def+6 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Atk, 6),
                    new StatBoostEffect(StatName.Def, 6),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "mirror stance" => new BasicSkill(
                "Mirror Stance",
                "Si el rival inicia el combate, otorga Atk/Res+6 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Atk, 6),
                    new StatBoostEffect(StatName.Res, 6),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "steady posture" => new BasicSkill(
                "Steady Posture",
                "Si el rival inicia el combate, otorga Spd/Def+6 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Spd, 6),
                    new StatBoostEffect(StatName.Def, 6),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "swift stance" => new BasicSkill(
                "Swift Stance",
                "Si el rival inicia el combate, otorga Spd/Res+6 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Spd, 6),
                    new StatBoostEffect(StatName.Res, 6),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "bracing stance" => new BasicSkill(
                "Bracing Stance",
                "Si el rival inicia el combate, otorga Def/Res+6 durante el combate y " +
                "reduce el dan \u0303o del Follow-Up del rival en un 10 %.",
                new InitiatingCombatConditionRival(),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Def, 6),
                    new StatBoostEffect(StatName.Res, 6),
                    new FollowupAttackPercentReductionEffect(0.1)
                ])),
            "poetic justice" => new CompositeSkill(
                "Poetic Justic",
                "Inflige Spd-4 en el rival durante el combate y la unidad inflige dan \u0303o extra = 15 % " +
                "del ataque del rival.",
                new SkillList([
                    new BasicSkill(new AlwaysTrueCondition(), new EffectsList([new StatPenaltyOpponentEffect(StatName.Spd, 4)])),
                    new SecondDegreeSkill(new AlwaysTrueCondition(), new EffectsList([new RivalAtkFlatAttackBoostEffect(0.15)]))
                ])
                ),
            "laguz friend" => new BasicSkill(
                "Laguz friend",
                "La unidad recibe 50 % menos dan \u0303o, pero neutraliza todo Bonus a Def y Res y reduce " +
                "estos stats en un 50 % de su base. (Considere esta reducci \u0301on como un Penalty).,",
                new AlwaysTrueCondition(),
                new EffectsList([new LaguzFriendEffect()])
                ),
            "chivalry" => new BasicSkill(
                "Chivalry",
                "Si la unidad inicia el combate contra un rival con HP=100%," +
                "la unidad inflige +2 dan \u0303o en cada ataque y recibe -2 dan \u0303o por cada ataque del rival.",
                new AndCondition(
                    new ConditionsList([
                            new InitiatingCombatConditionSelf(), 
                            new HighHpConditionRival(1)]
                    )),
                new EffectsList(
                    [
                        new FlatAttackIncrementEffect(2),
                        new FlatDamageReductionEffect(2)
                    ])
                ),
            "dragon's wrath" => new CompositeSkill(
                "Dragon's Wrath",
                "Reduce el daño del primer ataque del rival durante el combate en un 25 %. " +
                "Si el Atk de la unidad > Res del rival, el primer ataque de la unidad hace daño extra = 25 %" +
                " del Atk de la unidad menos Res del rival.",
                new SkillList([
                        new BasicSkill(
                            new AlwaysTrueCondition(), 
                            new EffectsList(
                                [new FirstAttackPercentReductionEffect(0.25)])),
                        new SecondDegreeSkill(
                            new StatComparisonCondition(StatName.Atk, StatName.Res), 
                            new EffectsList(
                                [new DragonsWrathEffect()])
                            )])
                ),
            "prescience" => new CompositeSkill(
                "Prescience",
                "Inflige Atk/Res-5 en el rival durante el combate. Si la unidad inicia el combate o si el " +
                "rival usa magia o arcos, reduce el dan \u0303o del primer ataque del rival en un 30 %.",
                new SkillList([
                    new BasicSkill(new AlwaysTrueCondition(), new EffectsList(
                        [
                            new StatPenaltyOpponentEffect(StatName.Atk, 5),
                            new StatPenaltyOpponentEffect(StatName.Res, 5)
                        ])),
                    new BasicSkill(
                        new OrCondition(
                            new ConditionsList(
                            [
                                new InitiatingCombatConditionSelf(),
                                new UsingSpecificWeaponConditionRival(WeaponName.Bow),
                                new UsingSpecificWeaponConditionRival(WeaponName.Magic)
                            ])),
                        new EffectsList([new FirstAttackPercentReductionEffect(.3)])
                        )
                ])),
            "extra chivalry" => new CompositeSkill(
                "Extra Chivalry",
                new SkillList(
                    [
                        new BasicSkill(new HighHpConditionRival(0.5), new EffectsList([
                            new StatPenaltyOpponentEffect(StatName.Atk, 5),
                            new StatPenaltyOpponentEffect(StatName.Spd, 5),
                            new StatPenaltyOpponentEffect(StatName.Def, 5),
                        ])),
                        new SecondDegreeSkill(new AlwaysTrueCondition(), new EffectsList([
                            new ExtraChivalryEffect(0.5)
                        ]))
                        
                    ])),
            "guard bearing" => new BasicSkill(
                "Guard bearing",
                "Inflige Spd/Def-4 en el rival. Reduce el dan \u0303o de los ataques del rival en X %, donde X es" +
                " 60 durante el primer combate de la unidad en que inicia el combate y durante el primer combate de " +
                "la unidad en que el rival inicia el combate. X es igual a 30 en cualquier otro caso.",
                new AlwaysTrueCondition(),
                new EffectsList(
                    [
                        new StatPenaltyOpponentEffect(StatName.Spd, 4),
                        new StatPenaltyOpponentEffect(StatName.Def, 4),
                        new GuardBearingEffect()

                    ])
                ),
            "divine recreation" => new CompositeSkill(
                "Divine recreation",
                new SkillList(
                    [
                    new BasicSkill(new HighHpConditionRival(0.5),new EffectsList([
                        new StatPenaltyOpponentEffect(StatName.Atk, 4),
                        new StatPenaltyOpponentEffect(StatName.Spd, 4),
                        new StatPenaltyOpponentEffect(StatName.Def, 4),
                        new StatPenaltyOpponentEffect(StatName.Res, 4),
                        new FirstAttackPercentReductionEffect(0.3)
                    ])),
                    new ThirdDegreeSkill(new HighHpConditionRival(0.5), new EffectsList([
                        new DivineRecreationEffect()
                    ]))
                    ])),
            
            _ => throw new SkillNotImplementedException()
        };
    }
}
