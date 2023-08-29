#!/bin/bash

while [[ "$#" -gt 0 ]]; do
    case $1 in
        --acr-token) acr_token="$2"; shift ;;
        --acr-instance) acr_instance="$2"; shift ;;
        --acr-username) acr_username="$2"; shift ;;
        *) echo "Unknown parameter passed: $1"; exit 1 ;;
    esac
    shift
done

# Build the Docker image
docker build -t pulseasset:1.0 -f PulseAsset/Dockerfile .

# Log into Azure ACR; token must be set in the environment
echo $acr_token | sudo docker login $acr_instance --username $acr_username --password-stdin

# Tag the image
docker tag pulseasset:1.0 ${acr_instance}/pulseasset:1.0

# Push to Azure ACR
docker push ${acr_instance}/pulseasset:1.0

# Deployment will automatically occur on the Azure side
