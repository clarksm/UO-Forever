
////////////////////////////////////////
//                                    //
//   Generated by CEO's YAAAG - V1.2  //
// (Yet Another Arya Addon Generator) //
//                                    //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RoseRugWestAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14496, -1, -3, 0}, {14533, 2, 1, 0}, {14534, 1, 1, 0}// 1	2	3	
			, {14537, 2, 0, 0}, {14538, 1, 0, 0}, {14541, 2, -1, 0}// 4	5	6	
			, {14542, 1, -1, 0}, {14544, 0, -1, 0}, {14540, 0, 0, 0}// 7	8	9	
			, {14536, 0, 1, 0}, {14550, 1, -3, 0}, {14545, 2, -2, 0}// 10	11	12	
			, {14546, 1, -2, 0}, {14549, 2, -3, 0}, {14552, 0, -3, 0}// 13	14	15	
			, {14548, 0, -2, 0}, {14532, 0, 2, 0}, {14530, 1, 2, 0}// 16	17	18	
			, {14529, 2, 2, 0}, {14528, 0, 3, 0}, {14527, 1, 3, 0}// 19	20	21	
			, {14526, 2, 3, 0}// 22	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new RoseRugWestAddonDeed();
			}
		}

		[ Constructable ]
		public RoseRugWestAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public RoseRugWestAddon( Serial serial ) : base( serial )
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

	public class RoseRugWestAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new RoseRugWestAddon();
			}
		}

		[Constructable]
		public RoseRugWestAddonDeed()
		{
			Name = "RoseRugWest";
		}

		public RoseRugWestAddonDeed( Serial serial ) : base( serial )
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