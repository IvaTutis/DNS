# Domain Name Server iliti simpl implementacija Gradskog Adresara

## Pričam ti priču

Želiš posjetiti prijateljicu, ali kako imaš nizak prostorni IQ, znaš samo njeno ime, a nemaš pojma adresu. 
To je kao da pitaš prijateljicu Kristinu koja iz nekog totalno non-creepy razloga se skoro uvijek sijeća gdje svi kolege iz razreda žive, ona ti kaže da se ništa ne brineš i da bude ona našla adresu, nađe adresu - i onda možete otići k Luciji.

Evo kako bi to izgledalo na internetu:

1. Ti (Korisnik) kažeš svom najdražem browseru na svom računalu (Kristini) da želiš posjetiti www.Lucija.hr.
2. Tvoje browser ti kaže iz svoje memorije ili - ako ne zna 🤔 - napravi par telefonskih poziva
3. Kristina prije ili kasnije dođe do adrese stranice (123.45.67.89).
4. Sad kad tvoje računalo zna adresu, možete otići tamo (🚶🚶🚶) i buljiti u Luciju. 

Naravno, kao i u razredu, postoji par pitanja:  
-  Kako Kristina zna gdje su sve te informacije? Tj. kako tvoj komp zna naciljati DNS servere sa dobrim pitanjima? 
-  Kako funkcionira komunikacija između Kristine i gradskog adresara? Kako ona to pita i browse-a adrese točno?

```mermaid
sequenceDiagram
    participant Korisnik as Ti
    participant Preglednik as Kristina (tvoj browser)
    participant Resolver as Kristinin mozak 
    participant Root DNS as Glavni gradski ured 
    participant TLD DNS as Ured za kvart
    participant Authoritative DNS as Lucijini roditelji 
    
    Korisnik->>Preglednik: Upiši www.lucija.hr
    Preglednik->>Resolver: Gdje živi Lucija?
    
    alt Kristina se sjeća
        Resolver->>Preglednik: Evo adrese iz sjećanja
    else Kristina ne zna
        Resolver->>Root DNS: Gdje je ured za .hr adrese?
        Root DNS->>Resolver: Evo ti adresa ureda za .hr
        Resolver->>TLD DNS: Gdje je ured za lucija.hr?
        TLD DNS->>Resolver: Evo ti kontakt Lucijinih roditelja
        Resolver->>Authoritative DNS: Gdje točno živi Lucija?
        Authoritative DNS->>Resolver: Evo ti točna adresa
        Resolver->>Preglednik: Evo Lucijine adrese
    end
    Preglednik->>Korisnik: Prikaži Lucijinu web stranicu
```

## In reality 

Prevedimo to sada na dobar stari dosadni internetski protokol: 

```mermaid
sequenceDiagram
    participant User
    participant Browser
    participant Resolver
    participant Root DNS
    participant TLD DNS
    participant Authoritative DNS

    User->>Browser: Enter www.example.com
    Browser->>Resolver: Lookup www.example.com
    
    alt Cache Hit
        Resolver->>Browser: Return cached IP
    else Cache Miss
        Resolver->>Root DNS: Query for .com DNS
        Root DNS->>Resolver: Respond with .com DNS
        Resolver->>TLD DNS: Query for example.com DNS
        TLD DNS->>Resolver: Respond with example.com DNS
        Resolver->>Authoritative DNS: Query for www.example.com
        Authoritative DNS->>Resolver: Respond with IP
        Resolver->>Browser: Return IP address
    end

    Browser->>User: Display website
```