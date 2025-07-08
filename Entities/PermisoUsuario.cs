using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
	[Table("PERMISO_USUARIO")]
	public class PermisoUsuario
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[Column("USUARIO_ID")]
		public int UsuarioId { get; set; }
		public Usuario Usuario { get; set; }


		[Required]
		[Column("MODULO")]
		[StringLength(100)]
		public string Modulo { get; set; }

		[Required]
		[Column("PERMISO")]
		[StringLength(100)]
		public string Permiso { get; set; }

		[Column("ACTIVO")]
		public bool Activo { get; set; }

		// Relaciones si las necesitas:
		// public Usuario Usuario { get; set; }
	}
}
