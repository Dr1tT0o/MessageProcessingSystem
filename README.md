# MessageProcessingSystem

System Description:
The system being developed is a message filtering and processing system for the Napier
Bank. Its primary function is to process and categorize incoming messages according to the
message type. Messages can be SMS, Tweets, Emails and Significant Incident Reports (SIR),
which are a special kind of email. The details of how each message type must be processed
are discussed in the report included. The system must allow users to enter messages through a Graphical
User Interface (GUI) directly, or by loading in messages from a text file. When a message is
entered and processed successfully, it must be re-displayed on screen in its processed form
and saved to an output file in JSON format. At the end of an input session, a user must be
able to view all processed messages, the list of mentions, the trending list and the list of SIRs.
Additionally, the system must validate user input and inform the user when invalid input is
entered by displaying an error message. The system must be easy to use, and it must have
high performance.


For additional information please read the report included in /Report


Running Instructions:
Option 1: Navigate to "/NBMFS" and open the file "Napier Message Service.sln" with VisualStudio, then run application from within VisualStudio.
Option 2: Navigate to "/NBMFS/Napier Message Service/obj/Debug" and open the file "Napier Message Service" - this should start the application directly


In the file message_examples you can find examples of messages in a valid format that can be used to test the application.
