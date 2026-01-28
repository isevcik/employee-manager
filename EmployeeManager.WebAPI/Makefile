# Azure Container App Deployment Configuration
# Override these variables by setting environment variables or passing them to make
# Example: make create-app APP_NAME=myapp RESOURCE_GROUP=myapp-rg

# Application Configuration
APP_NAME ?= myapp
RESOURCE_GROUP ?= myapp-rg
ENVIRONMENT ?= myapp-env

# Container Registry Configuration
REGISTRY_NAME ?= myappregistry123
REGISTRY_SERVER ?= $(REGISTRY_NAME).azurecr.io
IMAGE_NAME ?= myapp
IMAGE_TAG ?= latest
FULL_IMAGE_NAME = $(REGISTRY_SERVER)/$(IMAGE_NAME):$(IMAGE_TAG)

# Container App Configuration
TARGET_PORT ?= 8080
INGRESS ?= external
QUICKSTART_IMAGE ?= mcr.microsoft.com/k8se/quickstart:latest
LOCATION ?= westeurope

# Azure Identity
IDENTITY ?= system

.PHONY: help create-rg create-env create-app set-registry assign-acr-pull update-app deploy build push clean

# Default target - show help
help:
	@echo "Azure Container App Deployment Makefile"
	@echo ""
	@echo "Configuration:"
	@echo "  APP_NAME          = $(APP_NAME)"
	@echo "  RESOURCE_GROUP    = $(RESOURCE_GROUP)"
	@echo "  ENVIRONMENT       = $(ENVIRONMENT)"
	@echo "  REGISTRY_NAME     = $(REGISTRY_NAME)"
	@echo "  REGISTRY_SERVER   = $(REGISTRY_SERVER)"
	@echo "  IMAGE_NAME        = $(IMAGE_NAME)"
	@echo "  IMAGE_TAG         = $(IMAGE_TAG)"
	@echo "  FULL_IMAGE_NAME   = $(FULL_IMAGE_NAME)"
	@echo "  TARGET_PORT       = $(TARGET_PORT)"
	@echo ""
	@echo "Available targets:"
	@echo "  help          - Show this help message"
	@echo "  create-rg     - Create Azure Resource Group"
	@echo "  create-env    - Create Azure Container App Environment"
	@echo "  create-app    - Create Azure Container App with quickstart image"
	@echo "  set-registry  - Configure container registry for the app"
	@echo "  assign-acr-pull - Assign AcrPull role to container app identity"
	@echo "  update-app    - Update app with latest image from registry"
	@echo "  deploy        - Complete deployment (set-registry + update-app)"
	@echo "  build         - Build Docker image locally"
	@echo "  push          - Push Docker image to Azure Container Registry"
	@echo "  build-push    - Build and push Docker image"
	@echo "  full-deploy   - Build, push, and update app"
	@echo ""
	@echo "Examples:"
	@echo "  make create-rg"
	@echo "  make create-env"
	@echo "  make create-app"
	@echo "  make deploy"
	@echo "  make update-app IMAGE_TAG=v1.2.3"
	@echo "  make full-deploy IMAGE_TAG=v1.2.3"

# Create Azure Resource Group
create-rg:
	@echo "Creating Azure Resource Group: $(RESOURCE_GROUP)..."
	az group create \
		--name $(RESOURCE_GROUP) \
		--location $(LOCATION)
	@echo "✓ Resource Group created successfully"

# Create Azure Container App Environment
create-env:
	@echo "Creating Azure Container App Environment: $(ENVIRONMENT)..."
	az containerapp env create \
		--name $(ENVIRONMENT) \
		--resource-group $(RESOURCE_GROUP) \
		--location $(LOCATION)
	@echo "✓ Container App Environment created successfully"

# Create Azure Container App with quickstart image
create-app:
	@echo "Creating Azure Container App: $(APP_NAME)..."
	az containerapp create \
		--name $(APP_NAME) \
		--resource-group $(RESOURCE_GROUP) \
		--environment $(ENVIRONMENT) \
		--image $(QUICKSTART_IMAGE) \
		--target-port $(TARGET_PORT) \
		--ingress $(INGRESS)
	@echo "✓ Container App created successfully"

# Set container registry for the app
set-registry:
	@echo "Configuring container registry for $(APP_NAME)..."
	az containerapp registry set \
		--name $(APP_NAME) \
		--resource-group $(RESOURCE_GROUP) \
		--identity $(IDENTITY) \
		--server $(REGISTRY_SERVER)
	@echo "✓ Registry configured successfully"

# Assign AcrPull role to container app managed identity
assign-acr-pull:
	@echo "Assigning AcrPull role to $(APP_NAME) managed identity..."
	@principalId=$$(az containerapp show \
		--name $(APP_NAME) \
		--resource-group $(RESOURCE_GROUP) \
		--query "identity.principalId" \
		--output tsv); \
	registryId=$$(az acr show \
		--name $(REGISTRY_NAME) \
		--query id \
		--output tsv); \
	az role assignment create \
		--assignee $$principalId \
		--scope $$registryId \
		--role AcrPull
	@echo "✓ AcrPull role assigned successfully"

# Update container app with latest image
update-app:
	@echo "Updating $(APP_NAME) with image $(FULL_IMAGE_NAME)..."
	az containerapp update \
		--name $(APP_NAME) \
		--resource-group $(RESOURCE_GROUP) \
		--image $(FULL_IMAGE_NAME)
	@echo "✓ App updated successfully"

# Deploy: set registry and update app
deploy: set-registry update-app
	@echo "✓ Deployment complete"

# Build Docker image locally
build:
	@echo "Building Docker image: $(FULL_IMAGE_NAME)..."
	docker build -t $(FULL_IMAGE_NAME) .
	@echo "✓ Image built successfully"

# Push Docker image to Azure Container Registry
push:
	@echo "Pushing image to registry: $(FULL_IMAGE_NAME)..."
	docker push $(FULL_IMAGE_NAME)
	@echo "✓ Image pushed successfully"

# Build and push Docker image
build-push: build push
	@echo "✓ Build and push complete"

# Full deployment: build, push, and update app
full-deploy: build-push update-app
	@echo "✓ Full deployment complete"

# Delete the container app
delete-app:
	@echo "Deleting Azure Container App: $(APP_NAME)..."
	@read -p "Are you sure you want to delete $(APP_NAME)? [y/N] " -n 1 -r; \
	echo; \
	if [[ $$REPLY =~ ^[Yy]$$ ]]; then \
		az containerapp delete \
			--name $(APP_NAME) \
			--resource-group $(RESOURCE_GROUP) \
			--yes; \
		echo "✓ Container App deleted"; \
	else \
		echo "Deletion cancelled"; \
	fi

# Show container app details
show:
	@echo "Fetching details for $(APP_NAME)..."
	az containerapp show \
		--name $(APP_NAME) \
		--resource-group $(RESOURCE_GROUP) \
		--output table

# Show container app logs
logs:
	@echo "Fetching logs for $(APP_NAME)..."
	az containerapp logs show \
		--name $(APP_NAME) \
		--resource-group $(RESOURCE_GROUP) \
		--follow
