#include <ESP8266HTTPClient.h>

const char* serverUrl = "http://exemplo.com/api";

void enviarDadosParaServidor(float umidade, float temperatura) {
  HTTPClient http;

  // Construir a URL com os parâmetros de umidade e temperatura
  String url = String(serverUrl) + "?umidade=" + String(umidade) + "&temperatura=" + String(temperatura);

  http.begin(url);

  int httpCode = http.GET();
  if (httpCode > 0) {
    Serial.println(F("Solicitação enviada com sucesso"));
  } else {
    Serial.println(F("Erro na solicitação"));
  }

  http.end();
}
