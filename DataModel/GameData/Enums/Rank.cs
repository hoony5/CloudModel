public enum Rank
{
    None,
    Nine,
    Eight,
    Seven,
    Six,
    Five,
    Four,
    Three,
    Two,
    One,
    Fm,
    F,
    Fp,
    Em,
    E,
    Ep,
    Dm,
    D,
    Dp,
    Cm,
    C,
    Cp,
    Bm,
    B,
    Bp,
    Am,
    A,
    Ap,
    Sm,
    S,
    Sp,
    L
}

public static class RankUtility
{
    public static string RankToString(this Rank rank)
    {
        return rank switch
        {
            Rank.Nine => "9",
            Rank.Eight => "8",
            Rank.Seven => "7",
            Rank.Six => "6",
            Rank.Five => "5",
            Rank.Four => "4",
            Rank.Three => "3",
            Rank.Two => "2",
            Rank.One => "1",
            Rank.Fm => "F-",
            Rank.F => "F",
            Rank.Fp => "F+",
            Rank.Em => "E-",
            Rank.E => "E",
            Rank.Ep => "E+",
            Rank.Dm => "D-",
            Rank.D => "D",
            Rank.Dp => "D+",
            Rank.Cm => "C-",
            Rank.C => "C",
            Rank.Cp => "C+",
            Rank.Bm => "B-",
            Rank.B => "B",
            Rank.Bp => "B+",
            Rank.Am => "A-",
            Rank.A => "A",
            Rank.Ap => "A+",
            Rank.Sm => "S-",
            Rank.S => "S",
            Rank.Sp => "S+",
            Rank.L => "Legend",
            _ => ""
        };
    }

    public static Rank StringToRank(string str)
    {
        return str switch
        {
            "9" => Rank.Nine,
            "8" => Rank.Eight,
            "7" => Rank.Seven,
            "6" => Rank.Six,
            "5" => Rank.Five,
            "4" => Rank.Four,
            "3" => Rank.Three,
            "2" => Rank.Two,
            "1" => Rank.One,
            "F-" => Rank.Fm,
            "F" => Rank.F,
            "F+" => Rank.Fp,
            "E-" => Rank.Em,
            "E" => Rank.E,
            "E+" => Rank.Ep,
            "D-" => Rank.Dm,
            "D" => Rank.D,
            "D+" => Rank.Dp,
            "C-" => Rank.Cm,
            "C" => Rank.C,
            "C+" => Rank.Cp,
            "B-" => Rank.Bm,
            "B" => Rank.B,
            "B+" => Rank.Bp,
            "A-" => Rank.Am,
            "A" => Rank.A,
            "A+" => Rank.Ap,
            "S-" => Rank.Sm,
            "S" => Rank.S,
            "S+" => Rank.Sp,
            "Legend" => Rank.L,
            _ => Rank.None
        };

    }
}