# SharpHIBP
A C# Tool to gather information about email breaches

## Usage

Simple:
This will just outputs the name of the breach the email is located in using the --email you can give it a single email to look up
```
C:> SharpHIBP.exe --api [API KEY] --email example@google.com
Email: example@google.com
[
{
    "Name":"2844Breaches"
    }
]
```
Verbose Usage:
This will output more info about the breach the email is located in, this way you know if the information you are looking for is in it
```
SharpHIBP.exe --api [API KEY] --email example@google.com --verbose
Email: example@google.com
[
{
    "Name":"2844Breaches",
        "Title":"2,
        844 Separate Data Breaches",
        "Domain":"",
        "BreachDate":"2018-02-19",
        "AddedDate":"2018-02-26T10:06:02Z",
        "ModifiedDate":"2018-02-26T10:06:02Z",
        "PwnCount":80115532,
        "Description":"In February 2018,
         <a href=\"https://www.troyhunt.com/ive-just-added-2844-new-data-breaches-with-80m-records-to-have-i-been-pwned/\" target=\"_blank\" rel=\"noopener\">a massive collection of almost 3,
        000 alleged data breaches was found online</a>. Whilst some of the data had previously been seen in Have I Been Pwned,
         2,
        844 of the files consisting of more than 80 million unique email addresses had not previously been seen. Each file contained both an email address and plain text password and were consequently loaded as a single &quot;unverified&quot; data breach.",
        "LogoPath":"https://haveibeenpwned.com/Content/Images/PwnedLogos/List.png",
        "DataClasses":[
        "Email addresses",
            "Passwords"
        ],
        "IsVerified":false,
        "IsFabricated":false,
        "IsSensitive":false,
        "IsRetired":false,
        "IsSpamList":false,
        "IsMalware":false,
        "IsSubscriptionFree":false
    }
]
```

File list Usage:
You can pass it a file containing a list of emails as well the thread has been set by default each 7 seconds so to not get timed out this is based on the PWNED 1 Subscription
> [!TIP]
> This also has a Verbose Method
```
C:> SharpHIBP.exe --api [API KEY] --file emails.txt
Email: example@google.com
[
{
    "Name":"2844Breaches"
    }
]
```
