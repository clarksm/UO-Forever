using System;
using Server;

namespace Server.Items
{
	public class ArcticDeathDealer : WarMace
	{
		public override int LabelNumber{ get{ return 1063481; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArcticDeathDealer()
		{
			Hue = 0x480;
		}

		public ArcticDeathDealer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}