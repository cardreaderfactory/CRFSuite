/********************************************************************************
 * \copyright
 * Copyright 2009-2017, Card Reader Factory.  All rights were reserved.
 * From 2018 this code has been made PUBLIC DOMAIN.
 * This means that there are no longer any ownership rights such as copyright, trademark, or patent over this code.
 * This code can be modified, distributed, or sold even without any attribution by anyone.
 *
 * We would however be very grateful to anyone using this code in their product if you could add the line below into your product's documentation:
 * Special thanks to Nicholas Alexander Michael Webber, Terry Botten & all the staff working for Operation (Police) Academy. Without these people this code would not have been made public and the existance of this very product would be very much in doubt.
 *
 *******************************************************************************/

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace DevInfo
{
	/// <summary>
	/// Summary description for Class.
	/// </summary>


	class DeviceInfo
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		private const int DIGCF_PRESENT    = (0x00000002);
		private const int MAX_DEV_LEN = 1000;
		private const int SPDRP_FRIENDLYNAME = (0x0000000C);  // FriendlyName (R/W)
		private const int SPDRP_DEVICEDESC = (0x00000000);    // DeviceDesc (R/W)

		[StructLayout(LayoutKind.Sequential)]
			private class SP_DEVINFO_DATA
				{
				 public UInt32 cbSize;
				 public Guid   ClassGuid;
                 public UInt32 DevInst;    // DEVINST handle
                 public IntPtr Reserved;
				};

		[DllImport("setupapi.dll")]//
		private static extern Boolean
		  SetupDiClassGuidsFromNameA(string ClassN, ref Guid guids, 
			UInt32 ClassNameSize, ref UInt32 ReqSize);

		[DllImport("setupapi.dll")]
		private static extern IntPtr                //result HDEVINFO
		  SetupDiGetClassDevsA(ref Guid ClassGuid, UInt32 Enumerator,
			IntPtr	 hwndParent,  UInt32 Flags);

		[DllImport("setupapi.dll")]
		private static extern Boolean
		  SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, UInt32 MemberIndex,
			SP_DEVINFO_DATA	 DeviceInfoData);

		[DllImport("setupapi.dll")]
		private static extern Boolean
		  SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		[DllImport("setupapi.dll")]
		private static extern Boolean
		  SetupDiGetDeviceRegistryPropertyA(IntPtr DeviceInfoSet,
		  SP_DEVINFO_DATA	 DeviceInfoData, UInt32 Property,
		  UInt32   PropertyRegDataType, StringBuilder  PropertyBuffer,
		  UInt32 PropertyBufferSize, IntPtr RequiredSize);



		public static int EnumerateDevices(UInt32 DeviceIndex, string ClassName, StringBuilder DeviceName)
		{
		 UInt32 RequiredSize = 0;
		 Guid guid=Guid.Empty;
		 Guid[] guids=new Guid[1];
		 IntPtr NewDeviceInfoSet;
		 SP_DEVINFO_DATA DeviceInfoData= new SP_DEVINFO_DATA();


		 bool res=SetupDiClassGuidsFromNameA(ClassName,ref guids[0],RequiredSize,ref RequiredSize);
		 if(RequiredSize==0)
			   {
				//incorrect class name:
				DeviceName=new StringBuilder("");
				return -2;
			   }

		 if(!res)
		  {
		   guids=new Guid[RequiredSize];
		   res=SetupDiClassGuidsFromNameA(ClassName,ref guids[0],RequiredSize,ref RequiredSize);

		   if(!res || RequiredSize==0)
			   {
		   //incorrect class name:
				DeviceName=new StringBuilder("");
				return -2;
			   }
		  }

		 //get device info set for our device class
		 NewDeviceInfoSet=SetupDiGetClassDevsA(ref guids[0],0,IntPtr.Zero,DIGCF_PRESENT);
		 if( NewDeviceInfoSet.ToInt32() == -1 )
			   {
		  //device information is unavailable:
				DeviceName=new StringBuilder("");
				return -3;
			   }

            DeviceInfoData.cbSize = (UInt32)Marshal.SizeOf(DeviceInfoData);
            
			//is devices exist for class
			DeviceInfoData.DevInst=0;
			DeviceInfoData.ClassGuid=System.Guid.Empty;
			DeviceInfoData.Reserved=(IntPtr)0;

			res=SetupDiEnumDeviceInfo(NewDeviceInfoSet,
				   DeviceIndex,DeviceInfoData);
			if(!res) {
		 //no such device:
				SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
				DeviceName=new StringBuilder("");
				return -1;
			}



		DeviceName.Capacity=MAX_DEV_LEN;
		if(!SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet,DeviceInfoData,
		SPDRP_FRIENDLYNAME,0,DeviceName,MAX_DEV_LEN,IntPtr.Zero) )
		{
         res = SetupDiGetDeviceRegistryPropertyA(NewDeviceInfoSet,
		  DeviceInfoData,SPDRP_DEVICEDESC,0,DeviceName,MAX_DEV_LEN, IntPtr.Zero);
		 if(!res){
		 //incorrect device name:
				SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
				DeviceName=new StringBuilder("");
				return -4;
			}
		}
		 return 0;
		}


	}
}
