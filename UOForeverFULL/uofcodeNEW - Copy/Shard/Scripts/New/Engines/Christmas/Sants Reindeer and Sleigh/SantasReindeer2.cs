using System;

namespace Server.Items
{
 	[Flipable( 0x3A67, 0x3A68 )]
	public class SantasReindeer2 : Item
	{
		[Constructable]
		public SantasReindeer2() : this( 0 )
		{
		}

		[Constructable]
		public SantasReindeer2( int hue ) : base( 0x3A67 )
		{
			Weight = 5.0;
			Name = NameList.RandomName( "reindeer" );
			LootType = LootType.Blessed; 
		}

		public SantasReindeer2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
