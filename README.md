# the Platform - Welcome Chatbot
The Welcome Chatbot is a personalized AI Assistant dedicated to helping new users learn more about the Platform and it's different modules by asking questions that pertain to the Platform. 

# UI
![image](https://github.com/user-attachments/assets/cfb716de-e23a-4262-93e4-51755a0b4b05)

# Installation Steps
Steps to run the Angular Frontend:

(1) Clone the chatbot branch via: <br>
`git clone -b chatbot https://github.com/User826/chatbot.git`

(2) Set API key as Environment Variable
 
Open a terminal
Run :
### Windows
 
> setx OPENAI_API_KEY "your-api-key"
 
MacOs and Linux
> export OPENAI_API_KEY="your-api-key"
 
(3) In a terminal, go to the 'chatbot' folder
Run command 
> dotnet run

(4) To use the chatbot you must also have the master dashboard running:
Clone the master dashboard branch via <br>
`git clone -b "master-java-angular" https://github.com/NextGenerationForge/FullstackMasterDashboard/tree/master-java-angular
 
(5) In a terminal, go to the 'dashboard' folder
Run command 
> ng serve
