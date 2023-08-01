using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA3Digital.Shared
{
    public class ExerciseComment
    {
        public int Id { get; set; } = default!;
        public string ExerciseName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Comment { get; set; } = default!;
    }
}
