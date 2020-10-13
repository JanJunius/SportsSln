using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IStoreRepository
    {
        // IQueryable ist ähnlich wie IEnumerable, allerdings optimiert für die Datenabfrage und daher sinnvoll im Context des EF
        // Hinzukommen z.B. Funktionen zum leichteren Zugriff wie Skip, um Datensätze zu überlesen oder Take, um eine bestimmte Anzahl Sätze zu erhalten
        // Wichtiger Unterschied zu IEnumerable: Mit jeder Drüberlaufen über die Enumeration wird ein neues SQL an die DB geschickt!
        // Um dies zu verhindern, muss die Enumeration erst in eine Liste (.ToList()) umgewandelt werden
        IQueryable<Product> Products { get; }
    }
}
