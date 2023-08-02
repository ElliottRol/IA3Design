using IA3Digital.Shared;

namespace IA3Digital.Server.Models
{
	public class EquipementResponse
	{
		public EquipmentResult Result { get; set; } = default!;

	}

	public class EquipmentResult
	{
		public List<Equipment> Records { get; set; } = default!;

	}


}
