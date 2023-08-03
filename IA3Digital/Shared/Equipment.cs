using System.Text.Json.Serialization;

namespace IA3Digital.Shared
{
	public class Equipment
	{
		[JsonPropertyName("_Id")]
		public int Id { get; set; }

		[JsonPropertyName("CLASS")]
		public string Class { get; set; } = default!;

		[JsonPropertyName("FITNESS_EQ")]
		public string FitnessEquipment { get; set; } = default!;

		[JsonPropertyName("FINISH")]
		public string Finish { get; set; } = default!;

		[JsonPropertyName("FITNESS__1")]
		public string Material { get; set; } = default!;

		[JsonPropertyName("POINT_X")]
		public double X_Cordinate { get; set; } = default!;

		[JsonPropertyName("POINT_Y")]
		public double Y_Cordinate { get; set; } = default!;
	}
}
