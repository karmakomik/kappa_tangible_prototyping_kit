#include <CapacitiveSensor.h>

CapacitiveSensor   cs_4_2 = CapacitiveSensor(3,2); // 1M resistor between pins 4 & 8, pin 8 is sensor pin, add a wire and or foil
CapacitiveSensor   cs_4_5 = CapacitiveSensor(3,5);
CapacitiveSensor   cs_4_8 = CapacitiveSensor(3,A4);
CapacitiveSensor   cs_4_10 = CapacitiveSensor(3,A0);
CapacitiveSensor   cs_3_6 = CapacitiveSensor(4,6);
CapacitiveSensor   cs_3_7 = CapacitiveSensor(4,7);

int threshold1, threshold2, threshold3, threshold4, threshold5, threshold6;
bool stringComplete = false;  
String inputString = ""; 

void setup()                    
{
   cs_4_2.set_CS_AutocaL_Millis(0xFFFFFFFF);// turn off autocalibrate on channel 1 - just as an example
   Serial.begin(9600);
   //pinMode(7,OUTPUT);
   //pinMode(LED_BUILTIN, OUTPUT);
   threshold1 = 1000;
   threshold2 = 1000;
   threshold3 = 1000;
   threshold4 = 1000;
   threshold5 = 1000;
   threshold6 = 1000;
   inputString.reserve(200);
}

void loop()                    
{
   long start = millis();
   long total1 =  cs_4_2.capacitiveSensor(50);
   long total2 =  cs_4_5.capacitiveSensor(50);
   long total3 =  cs_4_8.capacitiveSensor(50);
   long total4 =  cs_4_10.capacitiveSensor(50);
   long total5 =  cs_3_6.capacitiveSensor(50);
   long total6 =  cs_3_7.capacitiveSensor(50);

     /*Serial.print("total1 : ");
    Serial.print(total1);
    Serial.print(", total2 : ");
    Serial.print(total2);
    Serial.print(", total3 : ");
    Serial.print(total3);
    Serial.print(", total4 : ");
    Serial.println(total4);*/
   
   if(total1 >= 1000)
   {      
      Serial.println("1\n");
      //Serial.println("a\n");
      delay(50);
   }
      
   if(total2 >= 1000)
   {      
      Serial.println("2\n");
      //Serial.println("b\n");
      delay(50); 
   }
    
   if(total3 >= 1000)
    {      
      Serial.println("{3\n");
      //Serial.println("c\n");
      delay(50);
    }
      
    if(total4 >= 500)
    {      
      Serial.println("4\n");
      //Serial.println("d\n");
      delay(50);
    }

    if(total5 >= 500)
    {      
      Serial.println("5\n");
      //Serial.println("d\n");
      delay(50);
    }

    if(total6 >= 500)
    {      
      Serial.println("6\n");
      //Serial.println("d\n");
      delay(50);
    }
    if (stringComplete) 
    {
      Serial.println(inputString);
      // clear the string:
      inputString = "";
      stringComplete = false;
    }
}

void serialEvent() 
{
  while (Serial.available()) 
  {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag so the main loop can
    // do something about it:
    if (inChar == '\n') 
    {
      stringComplete = true;
    }
  }
}
