terraform {
  backend "gcs" {
    bucket = "espresso-8c4ac-tfstate"
    prefix = "services"
  }
}
