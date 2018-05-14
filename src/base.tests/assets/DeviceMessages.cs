namespace Masha.Foundation.Tests
{
    using System;

    public class CreateDevice
    {
        public string BluetoothName { get; set; }
        public string SerialNumber { get; set; }
        public int Generation { get; set; }
    }

    public class DeviceRegistered
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
