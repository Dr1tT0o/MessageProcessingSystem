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


To test the application, enter and process some messages - below are examples of messages in a valid format that can be used for demonstration purposes:

S000000001
+44829918273
Hi Bob, we have a great sale on at the moment, 50% off!! Get down to our store or visit us here www.link.com whilst stocks last

S000000002
+67236462552
Good news! Your order has been delivered, we hope you like them – team them up with our beautiful summer dresses www.link.com. HAND.

S000000003
+7812636172
HRU, your next shift is on 29/03/2017 from 9.00am to 3.00pm. If you need to change this, please contact your line manager.

T000000001
@CocaCola
Time for a Royal Celebration! #Royalbaby

T000000002
@BestCompany
Congratulations to @RobJones in accounting for winning our #NFL football pool! GG

T000000003
@Law4you
Thinking about getting into law? 30-year City lawyer answers your questions next Friday at 12 p.m. #LawyeringUpQ&A

E000000001
noreply@topshop.com
welcome
Thank you for signing up to Topshop emails.
You'll now be the first to know about our latest collections, our in-store and online offers, and how we're styling the latest trends. Visit us today at https://topshop.com.

E000000002
marketing@oasis.uk
Sign up
Thank you for signing up to our emails.
Now you'll be the first to hear about all things Oasis before anybody else!
Visit www.oasis.com to start shopping. 

E000000003
police@metro.uk
SIR 12/05/18
22-72-87
Customer attack
A customer has just been attacked in our branch in Edinburgh earlier this morning. Keep updated about this issue at http://bbc.uk

