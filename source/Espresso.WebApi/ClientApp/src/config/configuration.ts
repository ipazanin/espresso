import { Environment } from './environment';

class Configuration {
  public readonly environment: Environment;

  public readonly settings: Record<string, unknown>;

  /**
   * Initializes new instance of configuration objects
   * and sets its properties with priority in order from environment variables,
   * settings.{environment}.json file or settings.json file
   */
  constructor(
    appSettings: Record<string, unknown>,
    appSettingsEnvironment: Record<string, unknown>,
    environment: Environment
  ) {
    this.environment = environment;
    this.settings = {
      ...appSettings,
      ...appSettingsEnvironment,
    };

    // if (Object.keys(this.settings).length === 0) {
    //   Configuration.throwErrorExpression('Missing configuration.');
    // }

    Object.keys(this.settings).forEach((key) => {
      Object.defineProperty(this, key, {
        value: appSettingsEnvironment[key] ?? appSettings[key],
      });
    });
  }

  public getProperty(property: string): unknown {
    if (!Object.prototype.hasOwnProperty.call(this.settings, property)) {
      throw new Error('Missing key.');
    }

    return this.settings[property];
  }

  public getServerUrl(): string {
    const serverUrlPropertyName = 'serverUrl';
    if (
      !Object.prototype.hasOwnProperty.call(
        this.settings,
        serverUrlPropertyName
      )
    ) {
      throw new Error(`Missing key ${serverUrlPropertyName}`);
    }

    return this.settings[serverUrlPropertyName] as string;
  }

  public getHeaders(): Record<string, string> {
    const headersPropertyName = 'headers';
    if (
      !Object.prototype.hasOwnProperty.call(this.settings, headersPropertyName)
    ) {
      throw new Error(`Missing key ${headersPropertyName}`);
    }

    return this.settings[headersPropertyName] as Record<string, string>;
  }
}
export { Configuration };
