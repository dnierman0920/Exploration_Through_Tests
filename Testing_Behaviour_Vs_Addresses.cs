namespace test;
using Xunit.Abstractions;

public class Testing_Behaviour_Vs_Addresses
{

    //  The field 'output' allows the method .Writeline to be called from it
    private readonly ITestOutputHelper output;

    // The public Method  UnitTest1 sets each instance of UnitTest1 to have the class output
    public Testing_Behaviour_Vs_Addresses(ITestOutputHelper output)
    {
        this.output = output;
    }
    // ---------------------------------------------------------------------------------

    [Fact]
    public void Test_Object_Address_Not_Null()
    {
        unsafe
        {
            object house = new object();
            object* objAddressPointer = &house;
            var address = (int)objAddressPointer;
            output.WriteLine($"House object's address: {address}");
            Assert.NotNull(address);
        }
    }
    [Fact]
    public void Test_Object_Not_Null()
    {

        object house = new object();
        output.WriteLine($"House object: {house}");
        Assert.NotNull(house);

    }
    // ---------------------------------------------------------------------------------
    [Fact]
    public void Test_Address_Of_Two_Instances_Of_Same_Object_Are_Not_Equal()
    {
        unsafe
        {
            object house1 = new object();
            object house2 = new object();
            object* house1AddressPointer = &house1;
            object* house2AddressPointer = &house2;
            var address1 = (int)house1AddressPointer;
            var address2 = (int)house2AddressPointer;
            output.WriteLine($"House object's address: {address1}");
            output.WriteLine($"House object's address: {address2}");
            Assert.NotEqual(address1, address2);
        }
    }
    [Fact]
    public void Test_Two_Instances_Of_Same_Object_Are_Not_Equal()
    {

        object house1 = new object();
        object house2 = new object();
        output.WriteLine($"House 1 object: {house1}");
        output.WriteLine($"House 2 object: {house2}");
        Assert.NotEqual(house1, house2);

    }

    // ---------------------------------------------------------------------------------
    [Fact]
    public void Test_Address_Of_Instance_And_Reference_To_Instance_Are_Not_Equal()
    // we are getting the address where the ref variable, not to the instance it is referencing
    {
        unsafe
        {
            object house = new object();
            object houseReference = house;
            object* houseAddressPointer = &house;
            object* houseReferenceAddressPointer = &houseReference;
            var address = (int)houseAddressPointer;
            var refAddress = (int)houseReferenceAddressPointer;
            output.WriteLine($"House object's address: {address}");
            output.WriteLine($"House object's address: {refAddress}");
            Assert.NotEqual(address, refAddress);
        }
    }
    [Fact]
    public void Test_Instance_And_Reference_To_Instance_Are_Equal()
    {

        object house = new object();
        object houseReference = house;
        output.WriteLine($"House object: {house}");
        output.WriteLine($"House reference object: {houseReference}");
        Assert.Equal(house, houseReference);
    }
    // ---------------------------------------------------------------------------------
    [Fact]
    public void Test_House_Instance_numberOfRooms_Address_Not_Equal_To_House_Instance_Reference_numberOfRooms_Address()
    {
        unsafe
        {
            House house = new House(5);
            House houseReference = house;
            int houseRooms = house.numberOfRooms;
            var houseReferenceRooms = houseReference.numberOfRooms;

            int* houseRoomsAddressPointer = &houseRooms;
            int* houseReferenceRoomsAddressPointer = &houseReferenceRooms;
            var roomAddress = (int)houseRoomsAddressPointer;
            var refRoomAddress = (int)houseReferenceRoomsAddressPointer;
            output.WriteLine($"House object's room address: {roomAddress}");
            output.WriteLine($"House object reference's room address: {refRoomAddress}");
            Assert.NotEqual(roomAddress, refRoomAddress);
        }
    }
    [Fact]
    public void Test_House_numberOfRooms_Equals_House_Instance_numberOfRooms()
    {

        House house = new House(12);
        House houseReference = house;
        houseReference.numberOfRooms = 13;
        output.WriteLine($"House object's number of room: {house.numberOfRooms}");
        output.WriteLine($"House object reference's number of rooms: {houseReference.numberOfRooms}");
        Assert.Equal(house.numberOfRooms, houseReference.numberOfRooms);
    }
    // ---------------------------------------------------------------------------------

    [Fact]
    public void Test_Two_Instances_Of_House_With_Same_Instance_Of_Room_Have_Different_Room_Addresses()
    {
        unsafe
        {
            Room myRoom = new Room("Dave"); // only one instance of my room!
            House houseA = new House(5, myRoom);
            House houseB = new House(3, myRoom);
            Room houseARoom = houseA.room;
            Room houseBRoom = houseB.room;

            Room* houseAroomAddressPointer = &houseARoom;
            Room* houseBroomAddressPointer = &houseBRoom;
            var houseARoomAddress = (int)houseAroomAddressPointer;
            var houseBRoomAddress = (int)houseBroomAddressPointer;
            output.WriteLine($"House A myRoom object's address: {houseARoomAddress}");
            output.WriteLine($"House B myRoom object's address: {houseBRoomAddress}");
            Assert.NotEqual(houseARoomAddress, houseBRoomAddress);
        }
    }
    [Fact]
    public void Test_Two_Instances_Of_House_With_Same_Instance_Of_Room_Have_Same_Room()
    {
        Room myRoom = new Room("Dave"); // only one instance of my room!
        House houseA = new House(5, myRoom);
        House houseB = new House(3, myRoom);
        Room houseARoom = houseA.room;
        Room houseBRoom = houseB.room;
        myRoom.guestName = "David";

        output.WriteLine($"House A myRoom guest name: {houseA.room.guestName}");
        output.WriteLine($"House B myRoom guest name: {houseB.room.guestName}");
        Assert.Equal(houseA.room.guestName, houseB.room.guestName);
    }

    // ---------------------------------------------------------------------------------

    public class House
    {
        public int numberOfRooms { get; set; }
        public Room room { get; }

        public House(int NumberOfRooms)
        {
            numberOfRooms = NumberOfRooms;
        }
        public House(int NumberOfRooms, Room Room)
        {
            numberOfRooms = NumberOfRooms;
            room = Room;
        }
    }
    public class Room
    {
        public string guestName { get; set; }

        public Room(string GuestName)
        {
            guestName = GuestName;
        }
    }

}
