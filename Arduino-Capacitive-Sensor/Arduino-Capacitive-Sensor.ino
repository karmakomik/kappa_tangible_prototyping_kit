
#include <CapacitiveSensor.h>

CapacitiveSensor   cs_3_2 = CapacitiveSensor(3,2); // 1M resistor between pins 4 & 8, pin 8 is sensor pin, add a wire and or foil
CapacitiveSensor   cs_3_5 = CapacitiveSensor(3,5);
CapacitiveSensor   cs_3_A5 = CapacitiveSensor(3,A5);
CapacitiveSensor   cs_3_A0 = CapacitiveSensor(3,A0);
CapacitiveSensor   cs_4_6 = CapacitiveSensor(4,6);
CapacitiveSensor   cs_4_7 = CapacitiveSensor(4,7);

int threshold1, threshold2, threshold3, threshold4, threshold5, threshold6;
bool stringComplete = false;  
String inputString = ""; 

const byte numChars = 32;
char receivedChars[numChars];
char tempChars[numChars];        // temporary array for use when parsing

// variables to hold the parsed data
char selectedConnector[numChars] = {0};
int connectorSensitivity = 0;
float floatFromPC = 0.0;

boolean newData = false;

//============

void setup() 
{
    //cs_4_2.set_CS_AutocaL_Millis(0xFFFFFFFF);// turn off autocalibrate on channel 1 - just as an example  
    Serial.begin(115200);
    threshold1 = 500;
    threshold2 = 500;
    threshold3 = 500;
    threshold4 = 500;
    threshold5 = 500;
    threshold6 = 500;
    inputString.reserve(200);
}

//============

void loop() 
{
   long total1 =  cs_3_2.capacitiveSensor(50);
   long total2 =  cs_3_5.capacitiveSensor(50);
   long total3 =  cs_3_A5.capacitiveSensor(50);
   long total4 =  cs_3_A0.capacitiveSensor(50);
   long total5 =  cs_4_6.capacitiveSensor(50);
   long total6 =  cs_4_7.capacitiveSensor(50);

     /*Serial.print("total1 : ");
    Serial.print(total1);
    Serial.print(", total2 : ");
    Serial.print(total2);
    Serial.print(", total3 : ");
    Serial.print(total3);
    Serial.print(", total4 : ");
    Serial.println(total4);*/

   if(total1 >= threshold1)
   {      
      Serial.println("1\n");
      //Serial.println("a\n");
      delay(50);
   }
      
   if(total2 >= threshold2)
   {      
      Serial.println("2\n");
      //Serial.println("b\n");
      delay(50); 
   }
    
   if(total3 >= threshold3)
    {      
      Serial.println("3\n");
      //Serial.println("c\n");
      delay(50);
    }
      
    if(total4 >= threshold4)
    {      
      Serial.println("4\n");
      //Serial.println("d\n");
      delay(50);
    }

    if(total5 >= threshold5)
    {      
      Serial.println("5\n");
      //Serial.println("d\n");
      delay(50);
    }

    if(total6 >= threshold6)
    {      
      Serial.println("6\n");
      //Serial.println("d\n");
      delay(50);
    }
    
    //Receive Serial Commands
    recvWithStartEndMarkers();
    if (newData == true) 
    {
        strcpy(tempChars, receivedChars);
            // this temporary copy is necessary to protect the original data
            //   because strtok() used in parseData() replaces the commas with \0
        parseData();
        showParsedData();
        newData = false;
    }
}

//============

void recvWithStartEndMarkers() 
{
    static boolean recvInProgress = false;
    static byte ndx = 0;
    char startMarker = '<';
    char endMarker = '>';
    char rc;

    while (Serial.available() > 0 && newData == false) {
        rc = Serial.read();

        if (recvInProgress == true) {
            if (rc != endMarker) {
                receivedChars[ndx] = rc;
                ndx++;
                if (ndx >= numChars) {
                    ndx = numChars - 1;
                }
            }
            else {
                receivedChars[ndx] = '\0'; // terminate the string
                recvInProgress = false;
                ndx = 0;
                newData = true;
            }
        }

        else if (rc == startMarker) {
            recvInProgress = true;
        }
    }
}

//============

void parseData() 
{      // split the data into its parts

    char * strtokIndx; // this is used by strtok() as an index

    strtokIndx = strtok(tempChars,",");      // get the first part - the string
    strcpy(selectedConnector, strtokIndx); // copy it to messageFromPC
    int connectorNum = atoi(strtokIndx);

    strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
    connectorSensitivity = atoi(strtokIndx);     // convert this part to an integer

    /*switch (connectorNum) 
    {
      case 1:
        threshold1 = connectorSensitivity; 
        break;
      case 2:
        threshold2 = connectorSensitivity; 
        break;
      case 3:
        threshold3 = connectorSensitivity; 
        break;
      case 4:
        threshold4 = connectorSensitivity; 
        break;
      case 5:
        threshold5 = connectorSensitivity; 
        break;
      case 6:
        threshold6 = connectorSensitivity; 
        break;                
      default:
        // statements
        break;
    }  */
 


    /*strtokIndx = strtok(NULL, ",");
    floatFromPC = atof(strtokIndx);     // convert this part to a float*/

}

//============

void showParsedData() 
{
    //Serial.print("Message ");
    //Serial.println(selectedConnector);
    //Serial.print("Integer ");
    //Serial.println(connectorSensitivity);
    //Serial.print("Float ");
    //Serial.println(floatFromPC);
}
