﻿#region Header
//   Vorspire    _,-'/-'/  BaseThrowableAtMobile.cs
//   .      __,-; ,'( '/
//    \.    `-.__`-._`:_,-._       _ , . ``
//     `:-._,------' ` _,`--` -: `_ , ` ,' :
//        `---..__,,--'  (C) 2014  ` -'. -'
//        #  Vita-Nex [http://core.vita-nex.com]  #
//  {o)xxx|===============-   #   -===============|xxx(o}
//        #        The MIT License (MIT)          #
#endregion

#region References
using System;

using Server;
using Server.Mobiles;
using Server.Targeting;
using VitaNex.Modules.AutoPvP;
using VitaNex.Modules.AutoPvP.Battles;
using VitaNex.Targets;
#endregion

namespace VitaNex.Items
{
	public enum ThrowableAtMobileDelivery : byte
	{
		None = 0x00,
		MoveToWorld = 0x01,
		AddToPack = 0x02
	}

	public abstract class BaseThrowableAtMobile<TMobile> : BaseThrowable<TMobile>
		where TMobile : Mobile
	{
		[CommandProperty(AccessLevel.GameMaster)]
		public virtual bool AllowDeadTarget { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual ThrowableAtMobileDelivery Delivery { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual bool Damages { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual int DamageMin { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual int DamageMax { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual bool Heals { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual int HealMin { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public virtual int HealMax { get; set; }

		public BaseThrowableAtMobile(int itemID, int amount = 1)
			: base(itemID, amount)
		{
			AllowDeadTarget = false;
			Delivery = ThrowableAtMobileDelivery.MoveToWorld;
			Damages = false;
			DamageMin = 0;
			DamageMax = 0;
			Heals = false;
			HealMin = 0;
			HealMax = 0;
		}

		public BaseThrowableAtMobile(Serial serial)
			: base(serial)
		{ }

		public override bool CanThrowAt(Mobile from, TMobile target, bool message)
		{
			if (from == null || from.Deleted || target == null || target.Deleted || !base.CanThrowAt(from, target, message))
			{
				return false;
			}

			if (!target.Alive && !AllowDeadTarget)
			{
				if (message)
				{
					from.SendMessage(37, "You can't throw the {0} at the dead.", Name);
				}

				return false;
			}

		    if (!(from is PlayerMobile))
		        return false;
		    if (target is PlayerMobile && AutoPvP.IsParticipant(from as PlayerMobile))
		    {
		        var battle = AutoPvP.FindBattle(from as PlayerMobile);
		        if (battle != null && battle.State != PvPBattleState.Running)
		        {
		            from.SendMessage(37, "You cannot use this item until the battle commences.");
		            return false;
		        }
                if (battle != null && battle.State == PvPBattleState.Running && battle.FindTeam(from as PlayerMobile) == battle.FindTeam(target as PlayerMobile) && !(battle is UOF_FFABattle))
                {
                    from.SendMessage(37, "You cannot use this item on team mates.");
                    return false;
                }

		    }

			if (target.Alive && Damages && !AutoPvP.IsParticipant(from as PlayerMobile) && !from.CanBeHarmful(target, false, true))
			{
				if (message)
				{
					from.SendMessage(37, "You can't harm them.");
				}

				return false;
			}

			if (target.Alive && Heals && !from.CanBeBeneficial(target, false, true))
			{
				if (message)
				{
					from.SendMessage(37, "You can't heal them.");
				}

				return false;
			}

			return true;
		}

		protected override void OnBeforeThrownAt(Mobile from, TMobile target)
		{
			if (from == null || from.Deleted || target == null || target.Deleted)
			{
				return;
			}

			if (target.Alive && Heals)
			{
				from.DoBeneficial(target);
			}

            if (target.Alive && Damages)
			{
				from.DoHarmful(target);
			}

			base.OnBeforeThrownAt(from, target);
		}

		protected override void OnThrownAt(Mobile from, TMobile target)
		{
			if (from == null || from.Deleted || target == null || target.Deleted)
			{
				return;
			}

			if (target.Alive && Damages)
			{
				target.Damage(Utility.RandomMinMax(DamageMin, DamageMax), from);
			}

			if (target.Alive && Heals)
			{
				target.Heal(Utility.RandomMinMax(HealMin, HealMax), from);
			}

			if (Delivery != ThrowableAtMobileDelivery.None)
			{
				var instance = VitaNexCore.TryCatchGet(() => GetType().CreateInstance<BaseThrowableAtMobile<TMobile>>());

				if (instance != null)
				{
					switch (Delivery)
					{
						case ThrowableAtMobileDelivery.MoveToWorld:
							instance.MoveToWorld(target.Location, target.Map);
							break;
						case ThrowableAtMobileDelivery.AddToPack:
							{
								if (!target.AddToBackpack(instance))
								{
									instance.MoveToWorld(target.Location, target.Map);
								}
							}
							break;
						default:
							instance.Delete();
							break;
					}
				}
			}

			base.OnThrownAt(from, target);
		}

		public override Target BeginTarget(Mobile from)
		{
			return new MobileSelectTarget<TMobile>(BeginThrow, src => { }, ThrowRange, false, TargetFlags);
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			int version = writer.SetVersion(1);

			switch (version)
			{
				case 1:
				case 0:
					{
						writer.Write(AllowDeadTarget);

						if (version < 1)
						{
							writer.Write((byte)Delivery);
						}
						else
						{
							writer.WriteFlag(Delivery);
						}

						writer.Write(Damages);
						writer.Write(DamageMin);
						writer.Write(DamageMax);
						writer.Write(Heals);
						writer.Write(HealMin);
						writer.Write(HealMax);
					}
					break;
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
				case 0:
					{
						AllowDeadTarget = reader.ReadBool();

						if (version < 1)
						{
							Delivery = (ThrowableAtMobileDelivery)reader.ReadByte();
						}
						else
						{
							Delivery = reader.ReadFlag<ThrowableAtMobileDelivery>();
						}

						Damages = reader.ReadBool();
						DamageMin = reader.ReadInt();
						DamageMax = reader.ReadInt();
						Heals = reader.ReadBool();
						HealMin = reader.ReadInt();
						HealMax = reader.ReadInt();
					}
					break;
			}
		}
	}
}