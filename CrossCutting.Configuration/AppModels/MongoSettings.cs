namespace CrossCutting.Configuration.AppModels;

public class MongoSettings
{
    public List<ConnectionSetting> ConnectionSettings { get; set; }
    public string Collection { get; set; }
}