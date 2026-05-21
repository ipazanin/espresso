spec:
  containers:
  - name: espresso-dashboard-instance-template
    image: europe-docker.pkg.dev/espresso-8c4ac/espresso/espresso-dashboard
    env:
    - name: ASPNETCORE_ENVIRONMENT
      value: production
    - name: DATABASE_NAME
      value: production
    - name: APIKEYSCONFIGURATION__PARSER
      value: ${api_key_parser}
    - name: DATABASECONFIGURATION__ESPRESSODATABASECONNECTIONSTRING
      value: ${espresso_db_connection_string}
    - name: DATABASECONFIGURATION__ESPRESSOIDENTITYDATABASECONNECTIONSTRING
      value: ${espresso_identity_db_connection_string}
    - name: APPCONFIGURATION__SENDGRIDAPIKEY
      value: ${sendgrid_api_key}
    - name: APPCONFIGURATION__ADMINUSERPASSWORD
      value: ${admin_user_password}
    - name: APPCONFIGURATION__SERVERURL
      value: ${server_url}
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
