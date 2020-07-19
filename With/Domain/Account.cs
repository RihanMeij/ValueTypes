﻿using System;
using Domain.ValueObjects;

namespace Domain
{
public class Account
{
    public readonly Money FinanceFee;
    public readonly TimeSpan Term;
    public readonly Money AdminFee;

    public Account(Money financeFee, TimeSpan term, Money adminFee)
    {
        _ = financeFee ?? throw new ArgumentNullException(nameof(financeFee));
        _ = adminFee ?? throw new ArgumentNullException(nameof(adminFee));

        if (adminFee.Currency != financeFee.Currency)
        {
            throw new ArgumentOutOfRangeException(nameof(financeFee), "Finance fee and Admin fee has to be the same currency");
        }

        if (term.Days < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(term),"Term is to short");
        }

        FinanceFee = financeFee;
        AdminFee = adminFee;
        Term = term;
    }
}
}
