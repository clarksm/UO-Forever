/////////////////////////////////////////////////
//
// Automatically generated by the
// AddonGenerator script by Arya
//
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MLTree01Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new MLTree01AddonDeed();
			}
		}

		[ Constructable ]
		public MLTree01Addon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 15070 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 15069 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 15068 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 15067 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 15066 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 15065 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 15064 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 15063 );
			AddComponent( ac, 2, 2, 0 );

		}

		public MLTree01Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class MLTree01AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new MLTree01Addon();
			}
		}

		[Constructable]
		public MLTree01AddonDeed()
		{
			Name = "MLTree01";
		}

		public MLTree01AddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}