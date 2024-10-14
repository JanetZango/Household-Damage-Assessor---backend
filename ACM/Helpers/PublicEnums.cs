namespace ACM.Helpers
{
    public class PublicEnums
    {
        public struct UserRoleList
        {
            public const string ROLE_ADMINISTRATOR = "ROLE_ADMINISTRATOR";
            public const string ROLE_USER = "ROLE_USER";
            public const string ROLE_TECHNICIAN_USER = "ROLE_TECHNICIAN_USER";
            public const string ROLE_SALES_USER = "ROLE_SALES_USER";
            public const string ROLE_VENDOR = "ROLE_VENDOR";
        }

        public enum AcmRoleList
        {
            FORM_DEFINITION_DELETE,
            FORM_DEFINITION_EDIT,
            FORM_DEFINITION_VIEW,
            WORKFLOWS_DELETE,
            WORKFLOWS_EDIT,
            WORKFLOWS_VIEW,
            NTF_GROUPS_DELETE,
            NTF_GROUPS_EDIT,
            NTF_GROUPS_VIEW,
            WORK_LIST_DELETE,
            WORK_LIST_ASSIGN_WORK,
            WORK_LIST_VIEW,
            WORK_LIST_OWN_WORK_ONLY,
            PERSONS_VIEW,
            PERSONS_EDIT,
            PERSONS_DELETE,
            WORK_LIST_PICK_UP_UNASSIGNED
        }

        public enum ClickatellChannelList
        {
            whatsapp,
            sms
        }

        public enum UIUpdateList
        {
            NOTIFICATIONS,
        }

        public enum LogLevel
        {
            LEVEL_INFORMATION,
            LEVEL_WARNING,
            LEVEL_EXCEPTION,
            LEVEL_CRITICAL
        }

        public enum DataAuditAction
        {
            ACTION_ADD,
            ACTION_UPDATE,
            ACTION_DELETE
        }

        public struct ChangeAction
        {
            public const string Created = "Created";
            public const string Updated = "Updated";
            public const string Removed = "Removed";
        }

        public enum TemporaryTokensTypeList
        {
            TYPE_FORGOT_PASSWORD,
            TYPE_EMAIL_VERIFICATION,
        }

        public enum AcmPersonBasicFieldList
        {
            FIRST_NAME,
            LAST_NAME,
            ID_NUMBER,
            PASSPORT_NUMBER,
            CURRENT_PLACEMENT,
            DATE_OF_BIRTH,
            OTHER
        }

        public enum EmailTemplateList
        {
            NTF_REGISTRATION_WELCOME_CUSTOM,
            NTF_PASSWORD_RESET_LINK,
            NTF_PASSWORD_CHANGED,
            NTF_CONTACT_US,
            NTF_EMAIL_VERIFICATION_LINK,
            NTF_UPCOMING_EVENT_REMINDER,
            NTF_REGISTRATION_APPROVAL_REQUIRED,
            NTF_ACCOUNT_APPROVED,
            NTF_FORM_SUBMISSION_QUESTION_NOTIFICATION
        }

        public enum SystemConfigurationList
        {
            KEY_LOGIN_TOKEN_VALID_MIN,
            KEY_PASSEORD_RESETLINK_VALIDFOR_MIN,
            KEY_LOGIN_RETRYLIMIT,
            KEY_CLEAN_APP_LOG_DAYS,
            KEY_DEFAULT_TIME_ZONE,

            KEY_PAYFAST_MERCHANTID,
            KEY_PAYFAST_MERCHANTKEY,
            KEY_PAYFAST_PASSPHRASE,
            KEY_PAYFAST_PROCESSURL,
            KEY_PAYFAST_VALIDATEURL,
            KEY_USE_PAYFAST_GATEWAY,
            KEY_PAYFAST_IS_TESTING,

            KEY_USE_BASIC_EFT,
            KEY_BASIC_EFT_DETAILS,
            KEY_CLICKATELLE_ENDPOINT,
            KEY_CLICKATELLE_API_KEY,
            KEY_CONTACT_US,
            KEY_TERMS_CONDITIONS,
            KEY_OTP_VALID_FOR_MINUTES,
        }

        public enum PageList
        {
            PAGE_USERS_INDEX,
            PAGE_MASTERDATA_EMAILTEMPLATELIST,
            PAGE_MASTERDATA_SYSTEMCONFIGLIST,
            PAGE_MASTERDATA_GENDERLIST,
            PAGE_MASTERDATA_ETHNICITYLIST,
            PAGE_MASTERDATA_SUBSCRIPTIONLIST,
            PAGE_MASTERDATA_FAQLIST,
            PAGE_MASTERDATA_COUNTRIESLIST,
            PAGE_MASTERDATA_LANGUAGECULTURELIST,
            PAGE_MASTERDATA_LOCALIZATIONVALUELIST,
            PAGE_MASTERDATA_PROVINCESLIST,
            PAGE_MASTERDATA_PROVINCELIST,
            PAGE_USERS_APPROVALREQUIRED,
            PAGE_MASTERDATA_ACMACCESSROLELIST,
            PAGE_ACM_PERSONSLIST,
            PAGE_FORMS_FORMDEFINITIONLIST,
            PAGE_FORMS_FORMINSTANCELIST,
        }

        public enum UserNotificationAction
        {
            NONE
        }

        public enum PaymentTypeList
        {
            PAYMENT_GATEWAY,
            MANUAL_PAYMENT_APPROVAL,
            SYSTEM
        }

        public enum TransactionType
        {
            WALLET_TOPUP,
            PAYOUT,
            COUPON_DISCOUNT,
            SYSTEM_FEE
        }

        public enum FormBuilderConditionActionsList
        {
            VISIBLE,
            HIDDEN,
            REQUIRED
        }

        public enum FormBuilderFormDefinitionLocationResponseTypeList
        {
            CUSTOM,
        }

        public enum FormBuilderFormDefinitionNumberUOMList
        {
            CELSIUS,
            JOULES,
            LITRES,
            METERS,
            LUMENS,
            NEWTON,
            VOLTS,
            WATTS,
            KILOGRAMS,
            AMPERES
        }

        public enum FormBuilderFormDefinitionStandardResponseTypeList
        {
            YES_NO,
            CUSTOM,
            OKAY_NOT_OKAY,
            COMPLIANT_NON_COMPLIANT,
            IN_ORDER_NOT_IN_ORDER,
            PASS_FAIL,
            FAULT_FOUND_FIXED,
            NOT_APPLICABLE_FAULT_FOUND_FIXED_ENG,
            NOT_APPLICABLE_FAULT_FOUND_FIXED_PORT,
            NOT_APPLICABLE_FAULT_FOUND_FIXED_SPAN
        }

        public enum FormBuilderFormDefinitionLibraryList
        {
            SECTION,
            CATEGORY,
            LOGIC,
            DYNAMIC,
            INFORMATION,
            QUESTION,
            NUMBER,
            SLIDER,
            CHECKBOX,
            TEXT,
            LOCATION,
            DRAWING,
            SIGNATURE,
            DATETIME,
            PICTURE
        }

        public enum FormInstanceStateList
        {
            STATE_DRAFT,
            STATE_SUBMITTED,
            STATE_APPROVED
        }

        public enum FormBuilderQuestionPostSubmissionActionList
        {
            UPDATE_PERSON_DATA
        }

        public struct LocalizationKeys
        {
            public const string Passport_Number = "Passport_Number";
            public const string Save_Draft = "Save_Draft";
            public const string Complete = "Complete";
            public const string Capture_end_time = "Capture_end_time";
            public const string Capture_start_time = "Capture_start_time";
            public const string Form_Definition = "Form_Definition";
            public const string Form_Questions = "Form_Questions";
            public const string Form_Items = "Form_Items";
            public const string Form_Submission_List = "Form_Submission_List";
            public const string Form_Submission_Details = "Form_Submission_Details";
            public const string Form_Submission_updated_successfully = "Form_Submission_updated_successfully";
            public const string Selected_Form_Definition = "Selected_Form_Definition";
            public const string Find_form_definition_by_searching_name_then_continue = "Find_form_definition_by_searching_name_then_continue";
            public const string Continue = "Continue";
            public const string Remove_Form_Submission_message = "Remove_Form_Submission_message";
            public const string Remove_Form_Submission = "Remove_Form_Submission";
            public const string Add_Form_Submission = "Add_Form_Submission";
            public const string Form_Instances = "Form_Instances";
            public const string State = "State";
            public const string Execution_Date = "Execution_Date";
            public const string Form_Instance_List = "Form_Instance_List";
            public const string Form_instance_approved_successfully = "Form_instance_approved_successfully";
            public const string Form_instance_removed_successfully = "Form_instance_removed_successfully";
            public const string Person_Details = "Person_Details";
            public const string Remove_Person_message = "Remove_Person_message";
            public const string Remove_Person = "Remove_Person";
            public const string Add_Person = "Add_Person";
            public const string Persons_list = "Persons_list";
            public const string Add_Form = "Add_Form";
            public const string Assigned_Forms = "Assigned_Forms";
            public const string Edited_By = "Edited_By";
            public const string Edited_date = "Edited_date";
            public const string Created_Date = "Created_Date";
            public const string Form_Builder = "Form_Builder";
            public const string Location_Relevant = "Location_Relevant";
            public const string Approval_Required = "Approval_Required";
            public const string Allow_Signatures = "Allow_Signatures";
            public const string Instructions = "Instructions";
            public const string Effective_EndDate = "Effective_EndDate";
            public const string Effective_StartDate = "Effective_StartDate";
            public const string Tags = "Tags";
            public const string Form_Details = "Form_Details";
            public const string FormDefinition_List = "FormDefinition_List";
            public const string FormDefinition_Details = "FormDefinition_Details";
            public const string FormDefinition_removed_successfully = "FormDefinition_removed_successfully";
            public const string FormDefinition_updated_successfully = "FormDefinition_updated_successfully";
            public const string Version = "Version";
            public const string Active = "Active";
            public const string Instance_Number = "Instance_Number";
            public const string Remove_Form_Definition_message = "Remove_Form_Definition_message";
            public const string Remove_Form_Definition = "Remove_Form_Definition";
            public const string Add_Form_Definition = "Add_Form_Definition";
            public const string Form_Definition_List = "Form_Definition_List";
            public const string Form_Definitions = "Form_Definitions";
            public const string Forms = "Forms";
            public const string Access_Role_Details = "Access_Role_Details";
            public const string Remove_Access_Role_message = "Remove_Access_Role_message";
            public const string Remove_Access_Role = "Remove_Access_Role";
            public const string Add_Access_Role = "Add_Access_Role";
            public const string Access_Role = "Access_Role";
            public const string Access_Role_List = "Access_Role_List";
            public const string Access_Roles = "Access_Roles";
            public const string Access_Role_removed_successfully = "Access_Role_removed_successfully";
            public const string Acm_Access_Role_updated_successfully = "Acm_Access_Role_updated_successfully";
            public const string Subject = "Subject";
            public const string or_click_to_pick_manually = "or_click_to_pick_manually";
            public const string Drop_files_to_upload = "Drop_files_to_upload";
            public const string Download_bulk_XLSX_template = "Download_bulk_XLSX_template";
            public const string Bulk_Upload = "Bulk_Upload";
            public const string File_was_not_uploaded = "File_was_not_uploaded";
            public const string Icon_Class_Name = "Icon_Class_Name";
            public const string Order = "Order";
            public const string Country = "Country";
            public const string Select_File_To_Upload = "Select_File_To_Upload";
            public const string Wallet_Amount = "Wallet_Amount";
            public const string To = "To";
            public const string From = "From";
            public const string ReferenceNumber = "Reference Number";
            public const string PF_Payment_Status = "PF_Payment_Status";
            public const string PF_Reference_ID = "PF_Reference_ID";
            public const string PF_Payment_ID = "PF_Payment_ID";
            public const string Amount_Gross = "Amount_Gross";
            public const string Transaction_Type = "Transaction_Type";
            public const string Payment_Type = "Payment_Type";
            public const string Item_Name = "Item_Name";
            public const string Transaction_Date = "Transaction_Date";
            public const string Payment_Transactions = "Payment_Transactions";
            public const string Pay_Now = "Pay_Now";
            public const string Wallet_Topup_Instruction = "Wallet_Topup_Instruction";
            public const string Topup_with = "Topup_with";
            public const string Custom_Amount = "Custom_Amount";
            public const string Payment_Completed_Message = "Payment_Completed_Message";
            public const string Wallet_Topup = "Wallet_Topup";
            public const string Payments = "Payments";
            public const string Payment_Completed = "Payment_Completed";
            public const string Payment_Cancelled_Pleasee_Try_Again = "Payment_Cancelled_Pleasee_Try_Again";
            public const string Unable_to_decline_user = "Unable_to_decline_user";
            public const string Unable_to_approve_user = "Unable_to_approve_user";
            public const string Decline_user_message = "Decline_user_message";
            public const string Decline_user = "Decline_user";
            public const string Decline = "Decline";
            public const string Approve_user_message = "Approve_user_message";
            public const string Approve_user = "Approve_user";
            public const string Approve = "Approve";
            public const string Users_Approval_List = "Users_Approval_List";
            public const string User_declined_successfully = "User_declined_successfully";
            public const string User_Approval = "User_Approval";
            public const string User_approved_successfully = "User_approved_successfully";
            public const string Admin_Approved = "Admin_Approved";
            public const string Meta_Values = "Meta_Values";
            public const string Enable_Reminder = "Enable_Reminder";
            public const string Url = "Url";
            public const string Date = "Date";
            public const string End_Time = "End_Time";
            public const string Start_Time = "Start_Time";
            public const string All_day_event = "All_day_event";
            public const string Calendar_Event = "Calendar_Event";
            public const string Save = "Save";
            public const string Add_Meta_Field = "Add_Meta_Field";
            public const string Meta_Fields = "Meta_Fields";
            public const string Color_Code = "Color_Code";
            public const string Event_Type = "Event_Type";
            public const string Add_Event_Type = "Add_Event_Type";
            public const string Standard_Events = "Standard_Events";
            public const string Calendar = "Calendar";
            public const string Event_updated_successfully = "Event_updated_successfully";
            public const string Event_removed_successfully = "Event_removed_successfully";
            public const string Event_type_removed_successfully = "Event_type_removed_successfully";
            public const string Event_types_updated_successfully = "Event_types_updated_successfully";
            public const string Comments = "Comments";
            public const string Frequently_Asked_Questions = "Frequently_Asked_Questions";
            public const string SMS_Body = "SMS_Body";
            public const string Account_Details = "Account_Details";
            public const string Account_suspended = "Account_suspended";
            public const string Access = "Access";
            public const string Actions = "Actions";
            public const string Add_Province = "Add_Province";
            public const string Add_Countries = "Add_Countries";
            public const string Add_FAQ = "Add_FAQ";
            public const string Add_Language_Culture = "Add_Language_Culture";
            public const string Add_Localization_Value = "Add_Localization_Value";
            public const string Add = "Add";
            public const string Administration = "Administration";
            public const string Agree = "Agree";
            public const string Alert = "Alert";
            public const string Back_To_DashBoard = "Back_To_DashBoard";
            public const string Back_to_list = "Back_to_list";
            public const string Body = "Body";
            public const string Cancel = "Cancel";
            public const string Catergory = "Catergory";
            public const string Category = "Category";
            public const string Cellphone_Number_Not_Valid = "Cellphone_Number_Not_Valid_For_Selected_Country";
            public const string Cellphone_Number_Cannot_Empty = "Cellphone_Number_Cannot_Be_Empty";
            public const string Cellphone_Number = "Cellphone_Number";
            public const string Cellphone_Verified = "Cellphone_Verified";
            public const string Clicking_The_Button_You_Agree_To_Terms_and_Conditions = "Clicking_The_Button_You_Agree_To_Terms_and_Conditions";
            public const string Contact_Us = "Contact_Us";
            public const string Content = "Content";
            public const string Config_Value = "Config_Value";
            public const string Configuration_updated_successfully = "Configuration_updated_successfully";
            public const string Contact_us = "Contact_us";
            public const string Contact_us_updated_successfully = "Contact_us_updated_successfully";
            public const string Contact_us_Details = "Contact_us_Details";
            public const string Copyright = "Copyright";
            public const string Countries = "Countries";
            public const string Country_Details = "Country_Details";
            public const string Countries_List = "Countries_List";
            public const string Country_updated_successfully = "Country_updated_successfully";
            public const string Country_removed_successfully = "Country_removed_successfully";
            public const string Created_By = "Created_By";
            public const string Culture_Code = "Culture_Code";
            public const string Curriculum_info = "Curriculum_info";
            public const string Dashboard = "Dashboard";
            public const string Default = "Default";
            public const string Description = "Description";
            public const string Details = "Details";
            public const string Display_Name = "Display_Name";
            public const string Edit_Profile = "Edit_Profile";
            public const string Edit = "Edit";
            public const string Email_Not_Valid_Format = "Email_Not_Valid_Format";
            public const string Email_Templates = "Email_Templates";
            public const string Email_Template_Details = "Email_Template_Details";
            public const string Email_Template_List = "Email_Template_List";
            public const string Email_Address = "Email_Address";
            public const string Email_Template_updated_successfully = "Email_Template_updated_successfully";
            public const string EmailAddress_Verified = "EmailAddress_Verified";
            public const string Ensure_Values_Match = "Ensure_Values_Match";
            public const string Enter_Text_To_Search_For = "Enter_Text_To_Search_For";
            public const string Event_Code = "Event_Code";
            public const string FAQ = "FAQ";
            public const string FAQ_Details = "FAQ_Details";
            public const string FAQ_List = "FAQ_List";
            public const string FAQ_updated_successfully = "FAQ_updated_successfully";
            public const string FAQ_removed_successfully = "FAQ_removed_successfully";
            public const string First_Name = "First_Name";
            public const string Generic_Error_Message = "Generic_Error_Message";
            public const string Guest = "Guest";
            public const string Home = "Home";
            public const string Identification_Number = "Identification_Number";
            public const string IDNumberValidationLength = "ID_Number_Length";
            public const string Insufficient_Access = "Insufficient_Access";
            public const string IsoAlpha3 = "Iso_Country_Code_3";
            public const string IsoAlpha2 = "Iso_Country_Code_2";
            public const string Key_Name = "Key_Name";
            public const string Languages = "Languages";
            public const string Language_Culture_Details = "Language_Culture_Details";
            public const string Language_Culture_List = "Language_Culture_List";
            public const string Language_Culture = "Language_Culture";
            public const string Language = "Language";
            public const string Language_culture_removed_successfully = "Language_culture_removed_successfully";
            public const string Language_Culture_removed_successfully = "Language_Culture_removed_successfully";
            public const string Language_Culture_updated_successfully = "Language_Culture_updated_successfully";
            public const string Localization_Values = "Localization_Values";
            public const string Localization_Value_Details = "Localization_Value_Details";
            public const string Localization_Value_List = "Localization_Value_List";
            public const string Localization_Value = "Localization_Value";
            public const string Localization_value_removed_successfully = "Localization_value_removed_successfully";
            public const string Localization_Value_removed_successfully = "Localization_Value_removed_successfully";
            public const string Localization_value_updated_successfully = "Localization_value_updated_successfully";
            public const string Logout = "Logout";
            public const string Log_In = "Log_In";
            public const string Mark_all_as_Read = "Mark_all_as_Read";
            public const string Master_Data = "Master_Data";
            public const string Menu = "Menu";
            public const string My_Notification_Details = "My_Notification_Details";
            public const string My_Notifications = "My_Notifications";
            public const string Name = "Name";
            public const string New = "New";
            public const string No_New_Notifications = "No_New_Notifications";
            public const string No_file_uploaded = "No_file_uploaded";
            public const string No_Results_Found = "No_Results_Found";
            public const string Notifications = "Notifications";
            public const string Notification_Details = "Notification_Details";
            public const string Owner = "Owner";
            public const string Page_Number = "Page_Number";
            public const string PhonePrefix = "Phone_Prefix";
            public const string Province_Not_Available = "Province_Not_Available_For_Selected_Country";
            public const string Province_Please_Select = "Please_Select_A_Province";
            public const string Province = "Province";
            public const string Province_Removed_Sucsess = "Province_Removed_Sucsess_Message";
            public const string Province_List = "Province_List";
            public const string Province_Detail_Select_Country = "Province_Details_Select_Country";
            public const string Province_Detail_Province_Iso = "Province_Iso_Code";
            public const string Province_Detail_Name = "Province_Name";
            public const string Province_Detail_Title = "Province_Details_Title";
            public const string Profile_Details_Not_Found = "Profile_Details_Not_Found";
            public const string Profile_Email_Dup_Error = "Email_Duplicate_Error";
            public const string Profile = "Profile";
            public const string Read_all_notifications = "Read_all_notifications";
            public const string Read = "Read";
            public const string Received = "Received";
            public const string ReceiveNotificationsTitle = "Receive_Notifications_Title";
            public const string ReceiveSmsNotifications = "SMS_Notifications";
            public const string ReciveEmailNotifications = "Email_Notifications";
            public const string Register = "Register";
            public const string Remove_Province_Message = "Remove_Province_Message";
            public const string Remove_Province = "Remove_Province";
            public const string Remove = "Remove";
            public const string Remove_country = "Remove_country";
            public const string Remove_country_message = "Remove_country_message";
            public const string Remove_FAQ = "Remove_FAQ";
            public const string Remove_FAQ_message = "Remove_FAQ_message";
            public const string Remove_Language_Culture = "Remove_Language_Culture";
            public const string Remove_Language_Culture_message = "Remove_Language_Culture_message";
            public const string Remove_Localization_Value = "Remove_Localization_Value";
            public const string Remove_Localization_Value_message = "Remove_Localization_Value_message";
            public const string Remove_user = "Remove_user";
            public const string Remove_user_message = "Remove_user_message";
            public const string Results = "Results";
            public const string Save_Changes = "Save_Changes";
            public const string Search = "Search";
            public const string Select_Country_Prov = "Select_A_Country_For_Province";
            public const string Select = "Select";
            public const string Status = "Status";
            public const string Success = "Success";
            public const string Surname = "Surname";
            public const string System_Settings = "System_Settings";
            public const string System_Settings_Details = "System_Settings_Details";
            public const string System_Settings_List = "System_Settings_List";
            public const string Template_tags = "Template_tags";
            public const string Template_Body = "Template_Body";
            public const string Terms_and_Conditions_Update_Fail = "Terms_and_Conditions_Update_Fail";
            public const string Terms_and_Conditions_Updated = "Terms_and_Conditions_Updated";
            public const string Terms_and_Conditions = "Terms_and_Conditions";
            public const string Timezone = "Timezone";
            public const string Title = "Title";
            public const string Unable_to_remove_user = "Unable_to_remove_user";
            public const string Unsaved_Changes_Please_save_before_navigating_away = "Unsaved_Changes_Please_save_before_navigating_away";
            public const string Uploaded = "Uploaded";
            public const string Upload_File = "Upload_File";
            public const string Upload = "Upload";
            public const string User_Profile_Title = "User_Profile_Title";
            public const string User_Profile_Update_Fail = "User_Profile_Update_Fail";
            public const string User_Profile_Update_Success = "User_Profile_Update_Success";
            public const string Users = "Users";
            public const string User_Details = "User_Details";
            public const string User_Roles = "User_Roles";
            public const string Users_List = "Users_List";
            public const string User_removed_successfully = "User_removed_successfully";
            public const string User_account_updated_successfully = "User_account_updated_successfully";
            public const string User = "User";
            public const string Validate_Display_Name = "Display_Name_Required";
            public const string Validate_Name_Title = "Name_Title_Required";
            public const string Validate_Last_Name = "Last_Name_Required";
            public const string Validate_First_Name = "First_Name_Required";
            public const string Value = "Value";
            public const string Validate_One_User_Role = "Validate_One_User_Role";
            public const string Validate_Email_Address = "Validate_Email_Address";
            public const string Validate_All_Required_Fields_User = "Validate_All_Required_Fields_User";
            public const string View_Privacy_Policy = "View_Privacy_Policy";
            public const string View_Terms_Conditions = "View_Terms_Conditions";
            public const string View = "View";
            public const string What_would_you_like_to_be_called = "What_would_you_like_to_be_called";
            public const string ACM = "ACM";
            public const string WorkList = "WorkList";
            public const string Persons = "Persons";
            public const string Person_Saved = "Person_Saved";
            public const string Person_Updated = "Person_Updated";
            public const string Person_Save_Failed = "Person_Save_Failed";
            public const string Person_Deleted = "Person_Deleted";
            public const string Person_Deletion_Failed = "Person_Deletion_Failed";
            public const string Person_List = "Person_List";
            public const string Person_Create = "Person_Create";
            public const string Person_Current_Placement = "Person_Current_Placement";
            public const string Date_Of_Birth = "Date_Of_Birth";
            public const string Person_Age = "Person_Age";
            public const string Basic_Details = "Basic_Details";
            public const string Advanced_Details = "Advanced_Details";
            public const string Reports_Documents = "Reports_Documents";
            public const string Change_Log = "Change_Log";
            public const string Years = "Years";
            public const string Months = "Months";
            public const string Gender = "Gender";
            public const string Gender_List = "Gender_List";
            public const string Saved = "Saved";
            public const string Confirm_Removal = "Confirm_Removal";
            public const string Removal_Successfull = "Removal_Succsessfull";
            public const string Update_Successfull = "Update_Successful";
            public const string List = "List";
            public const string Retrieval_Failed = "Retrieval_Failed";
            public const string Create_New = "Create_New";
            public const string Save_Failed = "Save_Failed";
            public const string Deletion_Failed = "Deletion_Failed";
            public const string Ethnicity = "Ethnicity";
            public const string Ethnicity_List = "Ethnicity_List";
            public const string Ethnicity_Details = "Ethnicity_Details";
            public const string Establishment = "Establishment";
            public const string Voucher = "Voucher";
            public const string Product = "Product";
            public const string Leads = "Leads";
            public const string School = "School";
            public const string School_Independent = "School Independent";
            public const string Health = "Health";
            public const string Government = "Government";
            public const string Post_Office = "Post Office";
            public const string Police = "Police";
            public const string Thusong = "Thusong";
            public const string Complex = "Complex";
            public const string Suburb = "Suburb";
            public const string Street_Number = "Street Number";
            public const string Street = "Street";
            public const string Scheduled_For_Installation = "Scheduled For Installation";
            public const string Scheduled_For_Installation_Date = "Scheduled For Installation Date";
            public const string Schedule = "Schedule";
            public const string Technicians = "Technicians";
            public const string Installation_Date = "Installation Date";
            public const string Job_Cards = "Job Cards";
            public const string CustomerCellphoneNumber = "Customer Cellphone Number";
            public const string IsInstalled = "Is It Installed?";
            public const string Installed_Jobs = "Installed Jobs";
            public const string ContactName = "Contact Name";
            public const string ContactNo = "Contact Number";
            public const string DcName = "DcName";
            public const string MnName = "MnName";
            public const string Voucher_Plan = "Voucher Plan";
            public const string VoucherCode = "Voucher Code";
            public const string ExpiryDate = "Expiry Date";
            public const string Price = "Price";
        }
    }
}