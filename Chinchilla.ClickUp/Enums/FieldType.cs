namespace Chinchilla.ClickUp.Enums
{
    /// <summary>
    /// Enum that rappresent the possible value of custom fieldType in a customField
    /// Supported types have been uncommented
    /// </summary>
    public enum FieldType
    {
        bad_type = -1, // not a real type
        url = 0,
        drop_down = 1,
        //email = 2,
        //phone = 3,
        //date = 4,
        //text = 5,
        //checkbox = 6,
        number = 7,
        //currency = 8,
        //tasks = 9,
        //users = 10,
        //emoji = 11,
        labels = 13,
        //automatic_progress = 14,
        //manual_progress = 15,
        //short_text = 16,
        list_relationship = 17,
    }

}
