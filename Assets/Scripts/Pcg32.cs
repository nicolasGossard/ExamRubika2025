using System;
using System.Diagnostics;

// Cette classe est publique et statique puisque l'on veut un générateur global unique utilisable directement
// avec "Pcg32..." dans n'importe quel script sans créer d'objet avec "new". L'inconvénient principal du fait
// que la classe soit statique est qu'il ne peut exister dans le jeu qu'un seul générateur global partagé
// partout, donc tous les scripts partagent la même suite aléatoire, mais dans ce jeu ce n'est pas un souci
// puisque GameManager est le seul script à utiliser de l'aléatoire et ceci peu de fois.
public static class Pcg32
{
    // state est la valeur interne actuelle du RNG qui change à chaque nouveau tirage
    private static ulong state;

    // inc est la constante de progression qui fait évoluer la valeur de state
    private static ulong inc;

    // La classe exécute automatiquement son constructeur statique seulement la première fois qu'on utilise
    // celle-ci, pour s'assurer que state et inc soient bien initialisés avant d'utiliser d'autres méthodes
    static Pcg32()
    {
        Init();
    }

    // Cette méthode fabrique automatiquement des valeurs de départ pour state et inc
    public static void Init()
    {
        // On tansforme l'heure actuelle précise en nombre pour crééer initState
        ulong initState = (ulong)DateTime.UtcNow.Ticks;

        // On récupère le numéro de processus de notre jeu Unity qui tourne
        ulong processId = (ulong)Process.GetCurrentProcess().Id;

        // On récupère la valeur actuelle d'une horloge haute précision du système
        ulong stopwatch = (ulong)Stopwatch.GetTimestamp();

        // On décale simplement processId dans la moitié haute du ulong, avant de la mélanger avec stopwatch
        // puisqu'il est une plus petite valeur que stopwatch et qu'il change en général beaucoup moins souvent
        ulong initSeq = (processId << 32) ^ stopwatch;

        // On transmet à Seed nos deux valeurs créées : initState pour construire state et initSeq pour construire inc
        Seed(initState, initSeq);
    }

    public static void Seed(ulong initState, ulong initSeq)
    {
        state = 0UL;
        inc = (initSeq << 1) | 1UL;

        NextUInt();
        state += initState;
        NextUInt();
    }

    public static uint NextUInt()
    {
        ulong oldState = state;

        state = oldState * 6364136223846793005UL + inc;

        uint xorshifted = (uint)(((oldState >> 18) ^ oldState) >> 27);
        int rot = (int)(oldState >> 59);

        return (xorshifted >> rot) | (xorshifted << ((32 - rot) & 31));
    }

    public static uint RangeUInt(uint max)
    {
        if (max == 0)
            return 0;

        uint threshold = unchecked((uint)(0 - max)) % max;

        uint r;
        do
        {
            r = NextUInt();
        }
        while (r < threshold);

        return r % max;
    }

    public static int RangeInt(int max)
    {
        if (max <= 0)
            return 0;

        return (int)RangeUInt((uint)max);
    }

    public static int RangeInt(int min, int max)
    {
        if (max <= min)
            return min;

        uint span = (uint)(max - min);
        return min + (int)RangeUInt(span);
    }

    public static float NextFloat()
    {
        return NextUInt() / 4294967296f;
    }

    public static float RangeFloat(float min, float max)
    {
        if (max <= min)
            return min;

        return min + NextFloat() * (max - min);
    }
}