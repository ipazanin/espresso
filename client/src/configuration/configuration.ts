import { Environment } from './environment';

class Configuration {
  /**
   * Environment
   */
  public readonly environment: Environment;

  /**
   * Configuration object
   */
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

    Object.keys(this.settings).forEach(key => {
      Object.defineProperty(this, key, {
        value: appSettingsEnvironment[key] ?? appSettings[key],
      });
    });
  }

  /**
   * Gets property from configuration object
   * @param property name of property on configuration object
   */
  public getProperty<T = unknown>(property: 'serverUrl' | 'headers'): T {
    if (!Object.prototype.hasOwnProperty.call(this.settings, property)) {
      throw new Error('Missing key.');
    }

    return this.settings[property] as T;
  }
}
export { Configuration };
