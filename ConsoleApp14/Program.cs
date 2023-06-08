// Singleton
public sealed class BibliotecaSingleton
{
    private static BibliotecaSingleton _instance;
    private static readonly object _lock = new object();

    private BibliotecaSingleton() { }

    public static BibliotecaSingleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new BibliotecaSingleton();
                }
                return _instance;
            }
        }
    }


    // Builder
    public class Carte
    {
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public int AnPublicare { get; set; }

        public override string ToString()
        {
            return $"Titlu: {Titlu}, Autor: {Autor}, An publicare: {AnPublicare}";
        }
    }

    public class CarteBuilder
    {
        private readonly Carte _carte;

        public CarteBuilder()
        {
            _carte = new Carte();
        }

        public CarteBuilder SetTitlu(string titlu)
        {
            _carte.Titlu = titlu;
            return this;
        }

        public CarteBuilder SetAutor(string autor)
        {
            _carte.Autor = autor;
            return this;
        }

        public CarteBuilder SetAnPublicare(int anPublicare)
        {
            _carte.AnPublicare = anPublicare;
            return this;
        }

        public Carte Build()
        {
            return _carte;
        }
    }


    // Adapter
    public interface IAdaptorCarte
    {
        void AdaugaCarte(string titlu, string autor, int anPublicare);
        void StergeCarte(string titlu);
    }

    public class GestiuneCarti
    {
        public void AdaugaCarte(Carte carte)
        {
            Console.WriteLine($"Cartea {carte} a fost adăugată în gestiune.");
        }

        public void StergeCarte(Carte carte)
        {
            Console.WriteLine($"Cartea {carte} a fost ștearsă din gestiune.");
        }
    }

    public class AdaptorGestiuneCarti : IAdaptorCarte
    {
        private readonly GestiuneCarti _gestiuneCarti;

        public AdaptorGestiuneCarti(GestiuneCarti gestiuneCarti)
        {
            _gestiuneCarti = gestiuneCarti;
        }

        public void AdaugaCarte(string titlu, string autor, int anPublicare)
        {
            Carte carte = new CarteBuilder()
                .SetTitlu(titlu)
                .SetAutor(autor)
                .SetAnPublicare(anPublicare)
                .Build();

            _gestiuneCarti.AdaugaCarte(carte);
        }

        public void StergeCarte(string titlu)
        {
            Carte carte = new CarteBuilder()
                .SetTitlu(titlu)
                .Build();

            _gestiuneCarti.StergeCarte(carte);
        }
    }


    // Decorator
    public interface IGestiuneCarti
    {
        void AdaugaCarte(Carte carte);
    }

    public class GestiuneCartiBase : IGestiuneCarti
    {
        public virtual void AdaugaCarte(Carte carte)
        {
            Console.WriteLine($"Cartea {carte} a fost adăugată în gestiune.");
        }
    }

    public class GestiuneCartiDecorator : IGestiuneCarti
    {
        private readonly IGestiuneCarti _gestiuneCarti;

        public GestiuneCartiDecorator(IGestiuneCarti gestiuneCarti)
        {
            _gestiuneCarti = gestiuneCarti;
        }

        public void AdaugaCarte(Carte carte)
        {
            Console.WriteLine("Se adaugă informații suplimentare pentru cartea:");
            Console.WriteLine($"Titlu: {carte.Titlu}");
            Console.WriteLine($"Autor: {carte.Autor}");
            Console.WriteLine($"An publicare: {carte.AnPublicare}");

            _gestiuneCarti.AdaugaCarte(carte);
        }
    }


    // Facade
    public class GestiuneCartiFacade
    {
        private readonly GestiuneCarti _gestiuneCarti;

        public GestiuneCartiFacade()
        {
            _gestiuneCarti = new GestiuneCarti();
        }

        public void AdaugaCarte(string titlu, string autor, int anPublicare)
        {
            Carte carte = new CarteBuilder()
                .SetTitlu(titlu)
                .SetAutor(autor)
                .SetAnPublicare(anPublicare)
                .Build();

            _gestiuneCarti.AdaugaCarte(carte);
        }

        public void StergeCarte(string titlu)
        {
            Carte carte = new CarteBuilder()
                .SetTitlu(titlu)
                .Build();

            _gestiuneCarti.StergeCarte(carte);
        }
    }


    // Proxy
    public interface IGestiuneCarti
    {
        void AdaugaCarte(Carte carte);
    }

    public class GestiuneCartiProxy : IGestiuneCarti
    {
        private readonly GestiuneCarti _gestiuneCarti;

        public GestiuneCartiProxy()
        {
            _gestiuneCarti = new GestiuneCarti();
        }

        public void AdaugaCarte(Carte carte)
        {
            if (VerificaDrepturiUtilizator())
            {
                _gestiuneCarti.AdaugaCarte(carte);
            }
            else
            {
                Console.WriteLine("Nu aveți drepturi suficiente pentru a adăuga o carte.");
            }
        }

        private bool VerificaDrepturiUtilizator()
        {
            // Verifică drepturile utilizatorului și returnează rezultatul
            return true;
        }
    }
}
