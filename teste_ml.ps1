# Script para testar endpoints de ML.NET
# Execute: .\teste_ml.ps1

$baseUrl = "http://localhost:5000"
$email = "ala@example.com"
$senha = "123456"

Write-Host "=== TESTE DE ML.NET ===" -ForegroundColor Cyan
Write-Host ""

# 1. Fazer login para obter token
Write-Host "1. Fazendo login..." -ForegroundColor Yellow
$loginBody = @{
    email = $email
    senha = $senha
} | ConvertTo-Json

$loginResponse = Invoke-RestMethod -Uri "$baseUrl/api/v2.0/Auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body $loginBody

$token = $loginResponse.token
Write-Host "✅ Token obtido com sucesso!" -ForegroundColor Green
Write-Host "Token: $($token.Substring(0, 50))..." -ForegroundColor Gray
Write-Host ""

# Configurar headers com token
$headers = @{
    "Authorization" = "Bearer $token"
    "Content-Type" = "application/json"
}

# 2. Obter informações do modelo
Write-Host "2. Obtendo informações do modelo..." -ForegroundColor Yellow
try {
    $modelInfo = Invoke-RestMethod -Uri "$baseUrl/api/v2.0/ML/model-info" `
        -Method GET `
        -Headers $headers
    
    Write-Host "✅ Informações do modelo obtidas!" -ForegroundColor Green
    Write-Host "Modelo: $($modelInfo.modelName)" -ForegroundColor Gray
    Write-Host "Versão: $($modelInfo.version)" -ForegroundColor Gray
    Write-Host "Algoritmo: $($modelInfo.algorithm)" -ForegroundColor Gray
    Write-Host "Features: $($modelInfo.features -join ', ')" -ForegroundColor Gray
    Write-Host ""
} catch {
    Write-Host "❌ Erro ao obter informações do modelo: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
}

# 3. Treinar modelo
Write-Host "3. Treinando modelo de ML..." -ForegroundColor Yellow
try {
    $trainResponse = Invoke-RestMethod -Uri "$baseUrl/api/v2.0/ML/train-model" `
        -Method POST `
        -Headers $headers
    
    if ($trainResponse.success) {
        Write-Host "✅ Modelo treinado com sucesso!" -ForegroundColor Green
        Write-Host "Acurácia: $($trainResponse.accuracy)" -ForegroundColor Gray
        Write-Host "Registros usados: $($trainResponse.recordsUsed)" -ForegroundColor Gray
        if ($trainResponse.metrics) {
            Write-Host "Macro Accuracy: $($trainResponse.metrics.macroAccuracy)" -ForegroundColor Gray
            Write-Host "Micro Accuracy: $($trainResponse.metrics.microAccuracy)" -ForegroundColor Gray
        }
    } else {
        Write-Host "⚠️ Treinamento falhou: $($trainResponse.message)" -ForegroundColor Yellow
    }
    Write-Host ""
} catch {
    Write-Host "❌ Erro ao treinar modelo: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
}

# 4. Fazer predição de status
Write-Host "4. Fazendo predição de status..." -ForegroundColor Yellow
$predictionBody = @{
    perfil = "ADMIN"
    tipoOperacao = "CHECK_IN"
    diasDesdeCriacao = 30
    totalOperacoes = 5
} | ConvertTo-Json

try {
    $predictionResponse = Invoke-RestMethod -Uri "$baseUrl/api/v2.0/ML/predict-status" `
        -Method POST `
        -Headers $headers `
        -Body $predictionBody
    
    if ($predictionResponse.success) {
        Write-Host "✅ Predição realizada com sucesso!" -ForegroundColor Green
        Write-Host "Status previsto: $($predictionResponse.predictedStatus)" -ForegroundColor Gray
        Write-Host "Confiança: $($predictionResponse.confidence)" -ForegroundColor Gray
        if ($predictionResponse.allScores) {
            Write-Host "Scores: $($predictionResponse.allScores -join ', ')" -ForegroundColor Gray
        }
    } else {
        Write-Host "⚠️ Predição falhou: $($predictionResponse.message)" -ForegroundColor Yellow
    }
    Write-Host ""
} catch {
    Write-Host "❌ Erro ao fazer predição: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
}

# 5. Analisar padrões
Write-Host "5. Analisando padrões de operações..." -ForegroundColor Yellow
try {
    $patternsResponse = Invoke-RestMethod -Uri "$baseUrl/api/v2.0/ML/analyze-patterns" `
        -Method GET `
        -Headers $headers
    
    if ($patternsResponse.success) {
        Write-Host "✅ Análise de padrões concluída!" -ForegroundColor Green
        Write-Host "Total de operações: $($patternsResponse.totalOperacoes)" -ForegroundColor Gray
        
        if ($patternsResponse.operacaoFrequencia) {
            Write-Host "`nFrequência por tipo:" -ForegroundColor Cyan
            $patternsResponse.operacaoFrequencia | ForEach-Object {
                Write-Host "  Tipo: $($_.tipo) - Quantidade: $($_.count)" -ForegroundColor Gray
            }
        }
        
        if ($patternsResponse.operacaoPorHora) {
            Write-Host "`nOperações por horário (Top 5):" -ForegroundColor Cyan
            $patternsResponse.operacaoPorHora | Select-Object -First 5 | ForEach-Object {
                Write-Host "  Hora $($_.hora): $($_.count) operações" -ForegroundColor Gray
            }
        }
        
        if ($patternsResponse.motosMaisAtivas) {
            Write-Host "`nMotos mais ativas (Top 5):" -ForegroundColor Cyan
            $patternsResponse.motosMaisAtivas | ForEach-Object {
                Write-Host "  Moto ID $($_.motoId): $($_.count) operações" -ForegroundColor Gray
            }
        }
    } else {
        Write-Host "⚠️ Análise falhou: $($patternsResponse.message)" -ForegroundColor Yellow
    }
    Write-Host ""
} catch {
    Write-Host "❌ Erro ao analisar padrões: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
}

Write-Host "=== TESTE CONCLUÍDO ===" -ForegroundColor Cyan


