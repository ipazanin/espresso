spec:
  containers:
  - name: espresso-web-api-vm-instance-template
    image: europe-docker.pkg.dev/espresso-8c4ac/espresso/espresso-webapi:${webapi_image_tag}
    env:
    - name: ASPNETCORE_ENVIRONMENT
      value: production
    - name: DATABASE_NAME
      value: production
    - name: APIKEYSCONFIGURATION__ANDROID
      value: ${api_key_android}
    - name: APIKEYSCONFIGURATION__IOS
      value: ${api_key_ios}
    - name: APIKEYSCONFIGURATION__WEB
      value: ${api_key_web}
    - name: APIKEYSCONFIGURATION__PARSER
      value: ${api_key_parser}
    - name: APIKEYSCONFIGURATION__DEVIOS
      value: ${api_key_dev_ios}
    - name: APIKEYSCONFIGURATION__DEVANDROID
      value: ${api_key_dev_android}
    - name: DATABASECONFIGURATION__ESPRESSODATABASECONNECTIONSTRING
      value: ${espresso_db_connection_string}
    - name: ASPNETCORE_URLS
      value: http://*:80
    - name: APPCONFIGURATION__ANALYTICSSLACKWEBHOOK
      value: ${slack_analytics_webhook}
    - name: APPCONFIGURATION__CRASHREPORTSLACKWEBHOOK
      value: ${slack_crash_report_webhook}
    - name: APPCONFIGURATION__NEWSOURCEREQUESTSLACKWEBHOOK
      value: ${slack_news_source_request_webhook}
    stdin: false
    tty: false
  restartPolicy: Always
# This container declaration format is not public API and may change without notice. Please
# use gcloud command-line tool or Google Cloud Console to run Containers on Google Compute Engine.
