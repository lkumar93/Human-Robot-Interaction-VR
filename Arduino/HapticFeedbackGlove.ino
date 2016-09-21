void setup() {
  // put your setup code here, to run once:
   Serial.begin(115200);
   pinMode(2,OUTPUT);
   pinMode(3,OUTPUT);
   pinMode(4,OUTPUT);
   pinMode(5,OUTPUT);
   pinMode(6,OUTPUT);
   pinMode(7,OUTPUT);


  // while(!Serial)
    digitalWrite(2,LOW);
    digitalWrite(3,LOW);
    digitalWrite(4,LOW);
    digitalWrite(5,LOW);
    digitalWrite(6,LOW);
    digitalWrite(7,LOW);
    
   
}

void loop()
{
   
  char getChare = ' ';
  if(Serial.available())
    {
      getChare = Serial.read();
      
      if(getChare == '1')
      {
        digitalWrite(4,HIGH);
      }
      
      if (getChare == '0')
      {
        digitalWrite(2,LOW);
        digitalWrite(3,LOW);
        digitalWrite(4,LOW);
        digitalWrite(5,LOW);
        digitalWrite(6,LOW);
        digitalWrite(7,LOW);
      }
      
       if(getChare == '2')
      {
        digitalWrite(2,HIGH);
        digitalWrite(3,HIGH);
        digitalWrite(4,HIGH);
        digitalWrite(5,HIGH);
        digitalWrite(6,HIGH);
        digitalWrite(7,HIGH);
      }
     
    }  
}
