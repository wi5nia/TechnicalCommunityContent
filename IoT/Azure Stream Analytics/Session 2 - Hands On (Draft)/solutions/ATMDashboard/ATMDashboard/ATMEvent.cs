// Copyright (c) 2015 Microsoft. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDashboard
{
    public class ATMEvent
    {
        public string CardNumber { get; set; }
        public string ATM1 { get; set; }
        public string ATM2 { get; set; }
        public string TransactionTime1 { get; set; }
        public string TransactionTime2 { get; set; }
    }
}
