using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Tickets;
//ESTO NO DEBERIA REPRESTENTAR ENTIDADES DE BASE DE DATOS SINO DE NEGOCIO


public sealed class Tickets : AggregateRoot
{
    public Tickets(int Id, string Nombre, string Descripcion, int User_Id, string Estado, int Departamento_Id, DateTime FechaDeCreacion, string Prioridad, Correo CreadoPor)
    {
        this.Id = Id;
        this.Nombre = Nombre;  // Usa "this" para evitar confusiones
        this.Descripcion = Descripcion;
        this.Estado = Estado;
        this.User_Id = User_Id ;
        this.Departamento_Id = Departamento_Id;
        this.FechaDeCreacion = FechaDeCreacion;
        this.Prioridad = Prioridad;
        this.CreadoPor = CreadoPor;
    }

    //esto solamente es para que EntityFramework pueda crear la tabla
    private Tickets() { }

    public static Tickets Create(int Id,string Nombre,  string Descripcion,
                                 string Estado, int Departamento_Id,int User_Id, DateTime FechaDeCreacion, string Prioridad, Correo CreadoPor)
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            throw new ArgumentException("El nombre de Tickets no puede estar vacío.");
        if (Id <= 0)
            throw new ArgumentException("El Id proporcionado no es válido.");
        
        if (Departamento_Id <= 0)
            throw new ArgumentException("El Id proporcionado no es válido.");

        if (Estado is null)
            throw new ArgumentException("El correo proporcionado no es válido.");

        if (Descripcion is null)
            throw new ArgumentException("La descripción no puede estar vacía.");

        if (User_Id <= 0)
            throw new ArgumentException("El User_Id no puede estar vacío o inválido.");
            
        if (FechaDeCreacion == null)
            throw new ArgumentException("La fecha de creación no puede estar vacía o inválida.");

        if (Prioridad is null)
            throw new ArgumentException("La prioridad no puede estar vacía o inválida.");

        if (CreadoPor is not Correo)
            throw new ArgumentException("El correo proporcionado no es válido.");

        return new Tickets(default, Nombre, Descripcion, User_Id,Estado, Departamento_Id, FechaDeCreacion,  Prioridad,CreadoPor
        );
    }


    public int Id { get; private set; }

    public string Nombre{ get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public string Estado { get; private set; } = string.Empty;
    public int Departamento_Id { get; private set; } = 0;
    public int User_Id { get; private set; } =0;
    public DateTime FechaDeCreacion { get; private set; } = DateTime.Now;


    public  string Prioridad { get; private set; } = string.Empty;
    public Correo? CreadoPor { get; private set; }

    public void Update(int Id, string Nombre, string Descripcion, string Estado, int Departamento_Id, DateTime FechaDeCreacion, int User_Id,  string Prioridad,Correo CreadoPor)
    {
        this.Id = Id;
        this.Nombre = Nombre;
        this.Descripcion = Descripcion;
        this.Estado = Estado;
        this.Departamento_Id = Departamento_Id;
        this.FechaDeCreacion = FechaDeCreacion;
        this.User_Id = User_Id;
        this.Prioridad = Prioridad;
        this.CreadoPor = CreadoPor;
    }
}