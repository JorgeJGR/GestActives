namespace GestActives
{
    public interface IPerson
    {
        string Name { get; set; }
        string Surname { get; set; }
        Company Enterprise { get; set; }
        string Telephone { get; set; }
        string Email { get; set; }
        string Discriminator { get; set; }
        string FullName();        
    }
}