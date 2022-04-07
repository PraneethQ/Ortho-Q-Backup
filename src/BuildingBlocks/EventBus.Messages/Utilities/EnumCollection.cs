using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Utilities
{
    public class EnumCollection
    {
        public enum EmailType
        {
            [Description("Subscription")]
            Subscription = 1,

            Registration = 2,

            [Description("Forgot Password")]
            ForgotPassword = 3,

            Appointment = 4,
            Payment = 5,
            [Description("Payment Confirmation")]
            PaymentConfirmation = 6
        }
    }
}
