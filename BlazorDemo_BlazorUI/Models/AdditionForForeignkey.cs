namespace BlazorDemo_BlazorUI.Models
{
    [Newtonsoft.Json.JsonObject(Title = "AdditionForForeignkey")]
    public class AdditionForForeignkey
    {
        public string TrackerID { get; set; }
        public List<string> AdditionIDs { get; set; }
    }
}
