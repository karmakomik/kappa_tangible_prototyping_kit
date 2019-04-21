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

void setup()                    
{
   //cs_4_2.set_CS_AutocaL_Millis(0xFFFFFFFF);// turn off autocalibrate on channel 1 - just as an example
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
   long total1 =  cs_3_2.capacitiveSensor(50);
   long total2 =  cs_3_5.capacitiveSensor(50);
   long total3 =  cs_3_A5.capacitiveSensor(50);
   long total4 =  cs_3_A0.capacitiveSensor(50);
   long total5 =  cs_4_6.capacitiveSensor(50);
   long total6 =  cs_4_7.capacitiveSensor(50);

    Serial.print("cs_3_2 : ");
    Serial.print(total1);
    Serial.print(", cs_3_5 : ");
    Serial.print(total2);
    Serial.print(", cs_3_A5 : ");
    Serial.print(total3);
    Serial.print(", cs_3_A0 : ");
    Serial.print(total4);
    Serial.print(", cs_4_6 : ");
    Serial.print(total5);
    Serial.print(", cs_4_7 : ");
    Serial.println(total6);   

}
