namespace EJournalManager.Helper
{
    public static class EnumHelper
    {
        public enum ResponseCodes
        {
            Approved_or_completed_successfully = 00,
            Refer_to_card_issuer = 01,
            Refer_to_card_issuer_special_condition = 02,
            Invalid_merchant = 03,

            Pickup_card = 04,


            Do_not_honor = 05,


            Error = 06,

            Pickup_card_special_condition = 07,


            Honor_with_identification = 08,
            Request_in_progress = 09,
            Approved,
            partial = 10,
            Approved_VIP = 11,
            Invalid_transaction = 12,
            Invalid_amount = 13,
            Invalid_card_number = 14,
            No_such_issuer = 15,
            Approved_update_track_3 = 16,

            Customer_cancellation = 17,

            Customer_dispute = 18,

            Reenter_transaction = 19,


            Invalid_response = 20,


            No_action_taken = 21,


            Suspected_malfunction = 22,


            Unacceptable_transaction_fee = 23,


            File_update_not_supported = 24,


            Unable_to_locate_record = 25,


            Duplicate_record = 26,


            File_update_edit_error = 27,


            File_update_file_locked = 28,


            File_update_failed = 29,


            Format_error = 30,


            Bank_not_supported = 31,


            Completed_partially = 32,


            Expired_card_pickup = 33,


            Suspected_fraud_pickup = 34,


            Contact_acquirer_pickup = 35,


            Restricted_card_pickup = 36,

            Call_acquirer_security_pickup = 37,

            PIN_tries_exceeded_pickup = 38,

            No_credit_account = 39,

            Function_not_supported = 40,

            Lost_card = 41,

            No_universal_account = 42,

            Stolen_card = 43,

            No_investment_account = 44,

            Not_sufficient_funds = 51,

            No_check_account = 52,

            No_savings_account = 53,

            Expired_card = 54,

            Incorrect_PIN = 55,

            No_card_record = 56,

            Transaction_not_permitted_to_cardholder = 57,

            Transaction_not_permitted_on_terminal = 58,

            Suspected_fraud = 59,

            Contact_acquirer = 60,


            Exceeds_withdrawal_limit = 61,

            Restricted_card = 62,

            Security_violation = 63,

            Original_amount_incorrect = 64,


            Exceeds_withdrawal_frequency = 65,


            Call_acquirer_security = 66,


            Hard_capture = 67,


            Response_received_too_late = 68,


            PIN_tries_exceeded = 75,

            Intervene,
            bank_approval_required = 77,


            Intervene_bank_approval_required_for_partial_amount = 78,


            Cutoff_in_progress = 90,


            Issuer_or_switch_inoperative = 91,


            Routing_error = 92,


            Violation_of_law = 93,


            Duplicate_transaction = 94,


            Reconcile_error = 95,


            System_malfunction = 96,


            Exceeds_cash_limit = 98,


            Reserved_future_Postilion_use = 99,
        }

        public enum TransactionTypes
        {
            Pending = 00,
            Wrong_Pin = 32,
            Success = 39,
            incomplete = 91,
            complete = 93,
            test = 21,
            test1 = 52,
            Balance_enquiry = 31,
            cash_withdrawal = 01,
            Mini_Statement = 38,
            Funds_Transfer = 40,
            payment_from_account = 50,
            Card_accounts_enquiry = 92
        }

        public enum UserRoles
        {
            sys_admin = 1,
            opration_mgr = 2,
            Branch_mgr=3,
            branch_officer=4,
            cash_officer=5,
            support=6,
            aco=7
        }
    }
}