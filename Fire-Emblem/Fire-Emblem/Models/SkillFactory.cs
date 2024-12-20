using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Exceptions;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Models.Skills.Conditions;
using Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;
using Fire_Emblem.Models.Skills.Conditions.TeamConditions;
using Fire_Emblem.Models.Skills.Conditions.WithBonusesConditions;
using Fire_Emblem.Models.Skills.Effects.BaseStatEffects;
using Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;
using Fire_Emblem.Models.Skills.Effects.NegatorsAndGuarantorsEffects;
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
                new AlwaysTrueCondition(),
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
                        new HighHpPercentCondition(0.25), 
                        new EffectsList([new StatPenaltyOpponentEffect(StatName.Atk, 5),
                            new StatPenaltyOpponentEffect(StatName.Spd, 5)])
                        ),
                    new SecondDegreeSkill(
                        new AndCondition(
                            new ConditionsList(
                                [
                                    new HighHpPercentCondition(0.25), 
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
                    new SecondDegreeSkill(new AlwaysTrueCondition(), new EffectsList([new FlatAttackIncrementGivenAtkEffect(0.15)]))
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
            "sol" => new BasicSkill(
                "Sol",
                " Por cada ataque, la unidad recupera HP=25 % del daño infligido.",
                new AlwaysTrueCondition(),
                new EffectsList([new HealingAfterAttackEffect(0.25)])
                ),
            "nosferatu" => new BasicSkill(
                "Nosferatu",
                "Al usar magia, la unidad recupera HP = 50 % del daño realizado por cada ataque.",
                new UsingSpecificWeaponConditionSelf(WeaponName.Magic), 
                new EffectsList([new HealingAfterAttackEffect(0.5)])),
            "solar brace" => new BasicSkill(
                "Solar Brace",
                "Si la unidad inicia el combate, recupera HP=50 % del daño realizado en cada ataque,",
                new InitiatingCombatConditionSelf(),
                new EffectsList([new HealingAfterAttackEffect(0.5)])
                ),
            "windsweep" => new BasicSkill(
                "Windsweep",
                "Si la unidad inicia el combate con una espada contra un rival que usa espadas, el " +
                "rival no podrá contraatacar.",
                new AndCondition(
                    new ConditionsList(
                        [
                            new InitiatingCombatConditionSelf(),
                            new UsingSpecificWeaponConditionSelf(WeaponName.Sword),
                            new UsingSpecificWeaponConditionRival(WeaponName.Sword)
                        ])),
                new EffectsList([new NegateCounterAttackEffect()])
                ),
            "surprise attack" => new BasicSkill(
                "Surprise Attack",
                "Si la unidad inicia el combate con un arco contra un rival que usa arcos, " +
                "el rival no podrá contraatacar.",
                new AndCondition(
                    new ConditionsList(
                        [
                            new InitiatingCombatConditionSelf(),
                            new UsingSpecificWeaponConditionSelf(WeaponName.Bow),
                            new UsingSpecificWeaponConditionRival(WeaponName.Bow)
                        ])),
                new EffectsList([ new NegateCounterAttackEffect()])
                ),
            "hliðskjálf" => new BasicSkill(
                "Hliðskjálf",
                "Si la unidad inicia el combate con magia contra un rival que usa magia, " +
                "el rival no podrá contraatacar",
                new AndCondition(
                    new ConditionsList(
                        [
                            new InitiatingCombatConditionSelf(),
                            new UsingSpecificWeaponConditionSelf(WeaponName.Magic),
                            new UsingSpecificWeaponConditionRival(WeaponName.Magic)
                        ])),
                new EffectsList([new NegateCounterAttackEffect()])
                ),
            "null c-disrupt" => new BasicSkill(
                "Null C-Disrupt",
                "Neutraliza los efectos que previenen los contraataques de la unidad.",
                new AlwaysTrueCondition(),
                new EffectsList([new NegateCounterAttackNegationEffect()])),
            "laws of sacae" => new CompositeSkill(
                "Laws of Sacae",
                new SkillList(
                    [
                        new BasicSkill(
                            new InitiatingCombatConditionSelf(), 
                            new EffectsList(
                                [
                                    new StatBoostEffect(StatName.Atk, 6),
                                    new StatBoostEffect(StatName.Def, 6),
                                    new StatBoostEffect(StatName.Res, 6),
                                    new StatBoostEffect(StatName.Spd, 6)
                        ])),
                        new SecondDegreeSkill(
                            new AndCondition(
                                new ConditionsList(
                                [
                                    new InitiatingCombatConditionSelf(),
                                    new StatComparisonCondition(StatName.Spd, StatName.Spd, 4),
                                    new OrCondition(
                                        new ConditionsList(
                                            [
                                                new UsingSpecificWeaponConditionRival(WeaponName.Lance),
                                                new UsingSpecificWeaponConditionRival(WeaponName.Axe),
                                                new UsingSpecificWeaponConditionRival(WeaponName.Sword)
                                            ]))])),
                            new EffectsList([new NegateCounterAttackEffect()])
                            )
                    ])),
            "eclipse brace" => new CompositeSkill(
                "Eclipse Brace",
                "Si la unidad inicia el combate, inflige daño extra=30% de la Def del rival en cada ataque físico" +
                " y recupera HP=50 % del daño realizado en cada ataque.",
                new SkillList(
                    [
                        new BasicSkill(
                            new InitiatingCombatConditionSelf(),
                            new EffectsList([new HealingAfterAttackEffect(0.5)])
                            ),
                        new SecondDegreeSkill(
                            new AndCondition(new ConditionsList(
                            [
                                new UsingPhysicalWeaponConditionSelf(), 
                                new InitiatingCombatConditionSelf()
                            ])),
                            new EffectsList([new LunarBraceEffect(0.3)])
                            )
                    ])),
            "resonance" => new BasicSkill(
                "Resonance",
                "Si la unidad usa magia y si HP de la unidad \u22652, la unidad pierde 1 HP al inicio del" +
                " combate e inflige +3 de daño por ataque durante el combate.",
                new AndCondition(new ConditionsList(
                    [
                        new UsingSpecificWeaponConditionSelf(WeaponName.Magic),
                        new HighHpFlatCondition(2)
                    ])),
                new EffectsList(
                    [
                        new BeforeCombatHpReductionEffect(1),
                        new FlatAttackIncrementEffect(3)
                    ])
                ),
            "flare" => new BasicSkill(
            "Flare",
            "Si la unidad usa magia, inflige Res-20 % en el rival (considere esto como un Penalty y" +
            " Res como el valor base) y la unidad recupera el 50 % del daño realizado por ataque.",
            new UsingSpecificWeaponConditionSelf(WeaponName.Magic),
            new EffectsList(
                [
                    new HealingAfterAttackEffect(0.5),
                    new StatPenaltyPercentageOpponentEffect(StatName.Res, 0.2)
                ])),
            
            "fury" => new BasicSkill(
                "Fury",
                "Otorga Atk/Spd/Def/Res+4. Después del combate, inflige 8 de daño en la unidad.",
                new AlwaysTrueCondition(),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Atk, 4),
                        new StatBoostEffect(StatName.Def, 4),
                        new StatBoostEffect(StatName.Res, 4),
                        new StatBoostEffect(StatName.Spd, 4),
                        new AfterCombatHpChangeEffect(-8)
                    ])),
            "mystic boost" => new BasicSkill(
                "Mystic Boost",
                "Inflige Atk-5 en el rival durante el combate. Restaura 10 HP a la unidad después del combate.",
                new AlwaysTrueCondition(),
                new EffectsList(
                    [
                        new StatPenaltyOpponentEffect(StatName.Atk, 5),
                        new AfterCombatHpChangeEffect(10)
                    ]
                    )),
            "atk/spd push" => new BasicSkill(
                "Atk/Spd Push",
                "Al inicio del combate, si el HP de la unidad \u2265 25 %, otorga Atk/Spd+7, " +
                "pero si la unidad atacó, inflige 5 de daño en la unidad después del combate.",
                new HighHpPercentCondition(0.25),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Atk, 7),
                        new StatBoostEffect(StatName.Spd, 7),
                        new AfterCombatHpChangeEffectIfAttacked(-5)
                    
                    ])
                ),
            "atk/def push" => new BasicSkill(
                "Atk/def Push",
                "Al inicio del combate, si el HP de la unidad \u2265 25 %, otorga Atk/def+7, " +
                "pero si la unidad atacó, inflige 5 de daño en la unidad después del combate.",
                new HighHpPercentCondition(0.25),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Atk, 7),
                    new StatBoostEffect(StatName.Def, 7),
                    new AfterCombatHpChangeEffectIfAttacked(-5)
                    
                ])
            ),
            "atk/res push" => new BasicSkill(
                "Atk/Res Push",
                "Al inicio del combate, si el HP de la unidad \u2265 25 %, otorga Atk/Res+7, " +
                "pero si la unidad atacó, inflige 5 de daño en la unidad después del combate.",
                new HighHpPercentCondition(0.25),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Atk, 7),
                    new StatBoostEffect(StatName.Res, 7),
                    new AfterCombatHpChangeEffectIfAttacked(-5)
                    
                ])
            ),
            "spd/def push" => new BasicSkill(
                "Spd/Def Push",
                "Al inicio del combate, si el HP de la unidad \u2265 25 %, otorga Spd/Def+7, " +
                "pero si la unidad atacó, inflige 5 de daño en la unidad después del combate.",
                new HighHpPercentCondition(0.25),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Spd, 7),
                    new StatBoostEffect(StatName.Def, 7),
                    new AfterCombatHpChangeEffectIfAttacked(-5)
                    
                ])
            ),
            "spd/res push" => new BasicSkill(
                "Spd/Res Push",
                "Al inicio del combate, si el HP de la unidad \u2265 25 %, otorga Spd/Res+7, " +
                "pero si la unidad atacó, inflige 5 de daño en la unidad después del combate.",
                new HighHpPercentCondition(0.25),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Spd, 7),
                    new StatBoostEffect(StatName.Res, 7),
                    new AfterCombatHpChangeEffectIfAttacked(-5)
                    
                ])
            ),
            "def/res push" => new BasicSkill(
                "Def/Res Push",
                "Al inicio del combate, si el HP de la unidad \u2265 25 %, otorga Def/Res+7, " +
                "pero si la unidad atacó, inflige 5 de daño en la unidad después del combate.",
                new HighHpPercentCondition(0.25),
                new EffectsList(
                [
                    new StatBoostEffect(StatName.Def, 7),
                    new StatBoostEffect(StatName.Res, 7),
                    new AfterCombatHpChangeEffectIfAttacked(-5)
                    
                ])
            ),
            "true dragon wall" => new CompositeSkill(
               "True Dragon Wall",
               new SkillList(
                   [
                       new SecondDegreeSkill(
                           new StatComparisonCondition(StatName.Res, StatName.Res),
                           new EffectsList([
                               new ScalingStatDamageReductionEffect(StatName.Res, 
                                   60, 6, "first"),
                               new ScalingStatDamageReductionEffect(StatName.Res, 40, 4, 
                                   "followup")
                           ])
                           ),
                       new BasicSkill(
                           new AlliedUsingWeaponCondition(WeaponName.Magic),
                           new EffectsList([
                               new AfterCombatHpChangeEffect(7)
                           ]))
                   ]
                   )),
            "scendscale" => new SecondDegreeSkill(
                "Scendscale",
                "Hace Daño extra por ataque = 25 % del Atk de la unidad, pero después del combate, " +
                "si la unidad atacó, inflige 7 de daño a la unidad.",
                new AlwaysTrueCondition(),
                new EffectsList(
                    [
                        new FlatAttackIncrementGivenAtkEffect(0.25, false),
                        new AfterCombatHpChangeEffectIfAttacked(-7)
                    ])
                ),
            "mastermind" => new CompositeSkill(
                "Mastermind",
                new SkillList(
                [
                    new BasicSkill(
                        new HighHpFlatCondition(2),
                        new EffectsList(
                            [
                                new BeforeCombatHpReductionEffect(1)
                            ])),
                    new BasicSkill(
                        new InitiatingCombatConditionSelf(),
                        new EffectsList(
                            [
                                new StatBoostEffect(StatName.Atk, 9),
                                new StatBoostEffect(StatName.Spd, 9)
                            ])),
                    new SecondDegreeSkill(
                        new InitiatingCombatConditionSelf(),
                        new EffectsList(
                        [
                           new MastermindEffect()
                        ]))
                ])),
            "bewitching tome" => new CompositeSkill(
                "Bewitching Tome",
                new SkillList(
                    [
                        new BasicSkill(
                            new OrCondition(new ConditionsList(
                                [
                                    new InitiatingCombatConditionSelf(),
                                    new UsingSpecificWeaponConditionRival(WeaponName.Magic),
                                    new UsingSpecificWeaponConditionRival(WeaponName.Bow)
                                ])),
                            new EffectsList(
                                [
                                    new StatBoostEffect(StatName.Atk, 5),
                                    new StatBoostEffect(StatName.Def, 5),
                                    new StatBoostEffect(StatName.Res, 5),
                                    new StatBoostEffect(StatName.Spd, 5),
                                    new StatBoostPercentEffect(StatName.Atk, StatName.Spd, 0.2),
                                    new StatBoostPercentEffect(StatName.Spd, StatName.Spd, 0.2),
                                    new FirstAttackPercentReductionEffect(0.3),
                                    new AfterCombatHpChangeEffect(7)

                                ])
                            ),
                        new SecondDegreeSkill(
                            new OrCondition(new ConditionsList(
                            [
                                new InitiatingCombatConditionSelf(),
                                new UsingSpecificWeaponConditionRival(WeaponName.Magic),
                                new UsingSpecificWeaponConditionRival(WeaponName.Bow)
                            ])),
                            new EffectsList(
                            [
                                new BewitchingTomeEffect()

                            ])
                        ),
                    ])),
            "quick riposte" => new BasicSkill(
                "Quick Riposte",
                "Si el HP de la unidad está al 60% o más y el rival inicia el combate, " +
                "la unidad realizará un follow-up garantizado.",
                new AndCondition(
                    new ConditionsList(
                        [
                            new HighHpPercentCondition(0.6),
                            new InitiatingCombatConditionRival()
                        ])),
                new EffectsList([new GuaranteedFollowupEffect()])
                ),
            "follow-up ring" => new BasicSkill(
                "Follow-up Ring",
                "Al inicio del combate, si el HP de la unidad >= 50 %, " +
                "la unidad puede realizar un Follow-Up garantizado.",
                new HighHpPercentCondition(0.5),
                new EffectsList([new GuaranteedFollowupEffect()])
                ),
            "wary fighter" => new BasicSkill(
                "Wary Fighter",
                "Si el HP de la unidad \u2265 50 % al inicio del combate," +
                " la unidad y el rival no pueden realizar un Follow-Up.",
                new HighHpPercentCondition(0.5),
                new EffectsList(
                    [
                        new NegateFollowupRivalEffect(),
                        new NegateFollowupSelfEffect()
                    ])
                ),
            "piercing tribute" => new BasicSkill(
                "Piercing Tribute",
                "Neutraliza los efectos que garantizan los Follow-Up del rival.",
                new AlwaysTrueCondition(),
                new EffectsList([new NegateGuaranteedFollowupEffect()])
                ),
            "mjölnir" => new BasicSkill(
                "Mjölnir",
                "Neutraliza los efectos que previenen los Follow-Up de la unidad.",
                new AlwaysTrueCondition(),
                new EffectsList([new NegateNegatedFollowupEffect()])
                ),
            "brash assault" => new SecondDegreeSkill(
                "Brash Assault",
                "Si el HP de la unidad \u2264 99 % y la unidad inicia el combate, o si el HP del rival = 100 % y" +
                " la unidad inicia el combate, inflige Def/Res-4 en el rival, reduce el daño del primer ataque del " +
                "rival en un 30 %, la unidad realiza un Follow-Up garantizado y hace daño extra en el siguiente ataque" +
                " de la unidad = 30 % del daño del primer ataque del rival antes de la reducción. Reinicia al final del" +
                " combate.\n",
                new OrCondition( new ConditionsList( 
                    [
                        new AndCondition( new ConditionsList(
                            [
                                new LowHpCondition(0.99),
                                new InitiatingCombatConditionSelf()
                            ])),
                        new AndCondition( new ConditionsList(
                            [
                                new HighHpConditionRival(1.00),
                                new InitiatingCombatConditionSelf()
                            ]))
                    ])),
                new EffectsList(
                    [
                        new StatPenaltyOpponentEffect(StatName.Def, 4),
                        new StatPenaltyOpponentEffect(StatName.Res, 4),
                        new BrashAssaultEffect(),
                        new FirstAttackPercentReductionEffect(0.3),
                        new GuaranteedFollowupEffect(),
                    ])
            ),
            "melee breaker" => new BasicSkill(
                "Melee Breaker",
                "Si el HP de la unidad está al 50 % o más al inicio del combate contra un rival que usa " +
                "espada, lanza o hacha, la unidad hace un follow-up garantizado " +
                "y el rival no puede realizar un follow-up.",
                new AndCondition( new ConditionsList(
                [
                    new HighHpPercentCondition(0.5),
                    new OrCondition(
                        new ConditionsList(
                            [
                                new UsingSpecificWeaponConditionRival(WeaponName.Sword),
                                new UsingSpecificWeaponConditionRival(WeaponName.Lance),
                                new UsingSpecificWeaponConditionRival(WeaponName.Axe)
                            ]))
                ])),
                new EffectsList(
                    [
                        new GuaranteedFollowupEffect(),
                        new NegateFollowupRivalEffect()
                    ])
                ),
            "range breaker" => new BasicSkill(
                "Range Breaker",
                "Si el HP de la unidad está al 50 % o más al inicio del combate contra un rival que usa magia" +
                " o arco, la unidad hace un follow-up garantizado y el rival no puede realizar un follow-up",
                new AndCondition( new ConditionsList(
                [
                    new HighHpPercentCondition(0.5),
                    new OrCondition(
                        new ConditionsList(
                        [
                            new UsingSpecificWeaponConditionRival(WeaponName.Magic),
                            new UsingSpecificWeaponConditionRival(WeaponName.Bow),
                        ]))
                ])),
                new EffectsList(
                [
                    new GuaranteedFollowupEffect(),
                    new NegateFollowupRivalEffect()
                ])
            ),
            "pegasus flight" => new CompositeSkill(
                "Pegasus Flight",
                new SkillList(
                    [
                        new BasicSkill(
                            new AlwaysTrueCondition(),
                            new EffectsList(
                                [
                                    new StatPenaltyOpponentEffect(StatName.Atk, 4),
                                    new StatPenaltyOpponentEffect(StatName.Def, 4)
                                ])
                            ),
                         new BasicSkill(
                            new BaseStatComparisonCondition(StatName.Spd, StatName.Spd, -10),
                            new EffectsList(
                                [
                                    new ScalingPenaltyEffect(StatName.Res, StatName.Atk, 
                                        0.8, 8),
                                    new ScalingPenaltyEffect(StatName.Res, StatName.Def, 
                                        0.8, 8)
                                ])
                            ),
                        new SecondDegreeSkill(
                            new AndCondition(new ConditionsList(
                            [
                                new BaseStatComparisonCondition(StatName.Spd, StatName.Spd, -10),
                                new DualStatComparisonCondition(StatName.Spd, StatName.Res, 
                                    StatName.Spd, StatName.Res, 0)

                            ])),
                            new EffectsList(
                            [
                                new NegateFollowupRivalEffect()
                            ]))
                    ])),
            "wyvern flight" => new CompositeSkill(
                "Wyvern Flight",
                new SkillList(
                [
                    new BasicSkill(
                        new AlwaysTrueCondition(),
                        new EffectsList(
                        [
                            new StatPenaltyOpponentEffect(StatName.Atk, 4),
                            new StatPenaltyOpponentEffect(StatName.Def, 4)
                        ])
                    ),
                    new BasicSkill(
                        new BaseStatComparisonCondition(StatName.Spd, StatName.Spd, -10),
                        new EffectsList(
                        [
                            new ScalingPenaltyEffect(StatName.Def, StatName.Atk, 
                                0.8, 8),
                            new ScalingPenaltyEffect(StatName.Def, StatName.Def, 
                                0.8, 8)
                        ])
                    ),
                    new SecondDegreeSkill(
                        new AndCondition(new ConditionsList(
                        [
                            new BaseStatComparisonCondition(StatName.Spd, StatName.Spd, -10),
                            new DualStatComparisonCondition(StatName.Spd, StatName.Def, 
                                StatName.Spd, StatName.Def, 0)

                        ])),
                        new EffectsList(
                        [
                            new NegateFollowupRivalEffect()
                        ]))
                ])),
            "null follow-up" => new BasicSkill(
                "Null Follow-Up",
                "Neutraliza los efectos que previenen los Follow-Up de la unidad y " +
                "que garantizan los Follow-Up del rival.",
                new AlwaysTrueCondition(),
                new EffectsList(
                    [
                        new NegateNegatedFollowupEffect(),
                        new NegateGuaranteedFollowupEffect()
                    ])),
            "sturdy impact" => new BasicSkill(
                "Sturdy Impact",
                "Si la unidad inicia el combate, otorga Atk+6, Def+10 y el rival no puede realizar un Follow-up.",
                new InitiatingCombatConditionSelf(),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Atk, 6),
                        new StatBoostEffect(StatName.Def, 10),
                        new NegateFollowupRivalEffect()
                        
                    ]
                )),
            "mirror impact" => new BasicSkill(
                "Mirror Impact",
                "Si la unidad inicia el combate, otorga Atk+6, Res+10 y el rival no puede realizar un Follow-up.",
                new InitiatingCombatConditionSelf(),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Atk, 6),
                        new StatBoostEffect(StatName.Res, 10),
                        new NegateFollowupRivalEffect()
                        
                    ]
                )),
            "swift impact" => new BasicSkill(
                "Swift Impact",
                "Si la unidad inicia el combate, otorga Spd+6, Res+10 y el rival no puede realizar un Follow-up.",
                new InitiatingCombatConditionSelf(),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Spd, 6),
                        new StatBoostEffect(StatName.Res, 10),
                        new NegateFollowupRivalEffect()
                        
                    ]
                )),
            "steady impact" => new BasicSkill(
                "Steady Impact",
                "Si la unidad inicia el combate, otorga Spd+6, Def+10 y el rival no puede realizar un Follow-up.",
                new InitiatingCombatConditionSelf(),
                new EffectsList(
                    [
                        new StatBoostEffect(StatName.Spd, 6),
                        new StatBoostEffect(StatName.Def, 10),
                        new NegateFollowupRivalEffect()
                        
                    ]
                )),
            "slick fighter" => new BasicSkill(
                "Slick Fighter",
                "Si el HP de la unidad \u2265 25 % y el rival inicia el combate, neutraliza los penalties " +
                "de la unidad durante el combate y la unidad realiza un Follow-Up garantizado.",
                new AndCondition(new ConditionsList(
                [
                    new HighHpPercentCondition(0.25),
                    new InitiatingCombatConditionRival()
                        
                ])),
                new EffectsList(
                [
                    new StatPenaltyNeutralizeEffect(StatName.Atk),
                    new StatPenaltyNeutralizeEffect(StatName.Def),
                    new StatPenaltyNeutralizeEffect(StatName.Spd),
                    new StatPenaltyNeutralizeEffect(StatName.Res),
                    new GuaranteedFollowupEffect()
                ])
            ),
            "wily fighter" => new BasicSkill(
                "Willy Fighter",
                "Si el HP de la unidad \u2265 25 % y el rival inicia el combate, neutraliza los bonus del " +
                "rival durante el combate y la unidad realiza un Follow-Up garantizado.",
                new AndCondition(new ConditionsList(
                [
                    new HighHpPercentCondition(0.25),
                    new InitiatingCombatConditionRival()
                        
                ])),
                new EffectsList(
                [
                    new StatBonusNeutralizeEffect(StatName.Atk),
                    new StatBonusNeutralizeEffect(StatName.Def),
                    new StatBonusNeutralizeEffect(StatName.Spd),
                    new StatBonusNeutralizeEffect(StatName.Res),
                    new GuaranteedFollowupEffect()
                ])
            ),
            "savvy fighter" => new CompositeSkill(
                "Savvy Fighter",
                new SkillList(
                    [
                        new BasicSkill(
                            new InitiatingCombatConditionRival(),
                            new EffectsList(
                                [
                                    new NegateGuaranteedFollowupEffect(),
                                    new NegateNegatedFollowupEffect()
                                ])),
                        new SecondDegreeSkill(
                            new AndCondition(new ConditionsList(
                                [
                                    new InitiatingCombatConditionRival(),
                                    new StatComparisonCondition(StatName.Spd, StatName.Spd, -4)
                                ])),
                            new EffectsList(
                            [
                                new FirstAttackPercentReductionEffect(0.3)]))
                    ])
            ),
            "flow force" => new BasicSkill(
                "Flow Force",
                "Si la unidad inicia el combate, neutraliza los efectos que previenen los Follow-Up de la unidad " +
                "y neutraliza los penaltis a Atk/Spd de la unidad durante el combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList(
                    [
                        new NegateNegatedFollowupEffect(),
                        new StatPenaltyNeutralizeEffect(StatName.Atk),
                        new StatPenaltyNeutralizeEffect(StatName.Spd)
                    ])),
            "flow refresh" => new BasicSkill(
                "Flow Refresh",
                "Si la unidad inicia el combate, neutraliza los efectos que previenen los Follow-Up de la unidad " +
                "y restaura 10 HP a la unidad después del combate.",
                new InitiatingCombatConditionSelf(),
                new EffectsList(
                    [
                      new NegateNegatedFollowupEffect(),
                      new AfterCombatHpChangeEffect(10)
                    ])),
            "flow flight" => new CompositeSkill(
                "Flow Flight",
                new SkillList(
                    [
                        new BasicSkill(
                            new InitiatingCombatConditionSelf(),
                            new EffectsList(
                                [
                                    new NegateNegatedFollowupEffect()
                                ])
                            ),
                        new SecondDegreeSkill(
                            new AndCondition(new ConditionsList(
                                [
                                    new StatComparisonCondition(StatName.Spd, StatName.Spd, -10),
                                    new InitiatingCombatConditionSelf()
                                ])),
                            new EffectsList(
                            [
                                new FlowEffect(StatName.Def, 0.7)
                            ])
                        ),
                        
                    ])),
            "flow feather" => new CompositeSkill(
                "Flow Feather",
                new SkillList(
                [
                    new BasicSkill(
                        new InitiatingCombatConditionSelf(),
                        new EffectsList(
                        [
                            new NegateNegatedFollowupEffect()
                        ])
                    ),
                    new SecondDegreeSkill(
                        new AndCondition(new ConditionsList(
                        [
                            new StatComparisonCondition(StatName.Spd, StatName.Spd, -10),
                            new InitiatingCombatConditionSelf()
                        ])),
                        new EffectsList(
                        [
                            new FlowEffect(StatName.Res, 0.7)
                        ])
                    ),
                        
                ])),
            "binding shield" => new CompositeSkill(
                "Binding Shield",
                new SkillList(
                    [
                        new SecondDegreeSkill(
                            new StatComparisonCondition(StatName.Spd, StatName.Spd, 5),
                            new EffectsList(
                                [
                                    new GuaranteedFollowupEffect(),
                                    new NegateFollowupRivalEffect()
                                ])
                            ),
                        new SecondDegreeSkill(
                            new  AndCondition(new ConditionsList(
                                [
                                    new StatComparisonCondition(StatName.Spd, StatName.Spd, 5),
                                    new InitiatingCombatConditionSelf(),
                                ])),
                            new EffectsList(
                            [
                                new NegateCounterAttackEffect()
                            ])
                        ),
                    ])),
            "sun-twin wing" => new BasicSkill(
                "Sun-Twin Wing",
                "Al inicio del combate, si el HP de la unidad \u2265 25 %, inflige Spd/Def-5 en el rival " +
                "y también neutraliza los efectos que garantizan los Follow-Up del rival y los efectos que " +
                "previenen los Follow-Up de la unidad durante el combate.",
                new HighHpPercentCondition(0.25),
                new EffectsList(
                    [
                        new StatPenaltyOpponentEffect(StatName.Spd, 5),
                        new StatPenaltyOpponentEffect(StatName.Def, 5),
                        new NegateGuaranteedFollowupEffect(),
                        new NegateNegatedFollowupEffect()
                    ])),
            "dragon's ire" => new CompositeSkill(
                "Dragon's Ire",
                new SkillList(
                    [
                        new BasicSkill(
                            new HighHpPercentCondition(0.25),
                            new EffectsList(
                                [
                                    new StatPenaltyOpponentEffect(StatName.Atk, 4),
                                    new StatPenaltyOpponentEffect(StatName.Res, 4),
                                    new GuaranteedFollowupEffect()
                                ])),
                        new BasicSkill(
                            new AndCondition(new ConditionsList(
                                [
                                    new HighHpPercentCondition(0.25),
                                    new InitiatingCombatConditionRival()
                                ])),
                            new EffectsList(
                            [
                                new NegateNegatedFollowupEffect()
                            ]))
                        
                    ])),
            "black eagle rule" => new CompositeSkill(
                "Black Eagle Rule",
                new SkillList(
                [
                    new BasicSkill(
                        new HighHpPercentCondition(0.25),
                        new EffectsList(
                        [
                            new GuaranteedFollowupEffect()
                        ])),
                    new BasicSkill(
                        new AndCondition(new ConditionsList(
                        [
                            new HighHpPercentCondition(0.25),
                            new InitiatingCombatConditionRival()
                        ])),
                        new EffectsList(
                        [
                            new FollowupAttackPercentReductionEffect(0.8)
                        ]))
                ])),
            "blue lion rule" => new CompositeSkill(
                "Blue Lion Rule",
                new SkillList(
                [
                    new BasicSkill(
                        new InitiatingCombatConditionRival(),
                        new EffectsList(
                        [
                            new GuaranteedFollowupEffect()
                        ])),
                    new SecondDegreeSkill(
                        new StatComparisonCondition(StatName.Def, StatName.Def),
                        new EffectsList(
                        [
                            new ScalingStatDamageReductionEffect(StatName.Def, 40)
                        ]))
                ])),
            "new divinity" => new CompositeSkill(
                "New Divinity",
                new SkillList(
                [
                    new BasicSkill(
                        new HighHpPercentCondition(0.25),
                        new EffectsList(
                        [
                            new StatPenaltyOpponentEffect(StatName.Atk, 5),
                            new StatPenaltyOpponentEffect(StatName.Res, 5)
                        ])),
                    new BasicSkill(
                        new HighHpPercentCondition(0.40),
                        new EffectsList(
                        [
                            new NegateFollowupRivalEffect()
                        ])),
                    new SecondDegreeSkill(
                        new AndCondition(new ConditionsList(
                        [
                            new HighHpPercentCondition(0.25),
                            new StatComparisonCondition(StatName.Res, StatName.Res)
                        ])),
                        new EffectsList(
                        [
                            new ScalingStatDamageReductionEffect(StatName.Res, 40)
                        ]))
                ])),
            "phys. null follow" => new CompositeSkill(
                "Phys. Null Follow",
                new SkillList(
                    [
                        new BasicSkill(new AlwaysTrueCondition(), new EffectsList(
                            [
                                new StatPenaltyOpponentEffect(StatName.Spd, 4),
                                new StatPenaltyOpponentEffect(StatName.Def, 4),
                                new NegateNegatedFollowupEffect(),
                                new NegateGuaranteedFollowupEffect(),
                            ])),
                        new ThirdDegreeSkill(new AlwaysTrueCondition(), new EffectsList(
                            [
                                new PercentDamageReductionPercentReductionEffect(0.5)
                            ]))
                ])),
            "mag. null follow" => new CompositeSkill(
                "Mag.. Null Follow",
                new SkillList(
                [
                    new BasicSkill(new AlwaysTrueCondition(), new EffectsList(
                    [
                        new StatPenaltyOpponentEffect(StatName.Spd, 4),
                        new StatPenaltyOpponentEffect(StatName.Res, 4),
                        new NegateNegatedFollowupEffect(),
                        new NegateGuaranteedFollowupEffect(),
                    ])),
                    new ThirdDegreeSkill(new AlwaysTrueCondition(), new EffectsList(
                    [
                        new PercentDamageReductionPercentReductionEffect(0.5)
                    ]))
                ])),

         
            _ => throw new SkillNotImplementedException()
        };
    }
}
